#Region "Author"
'Class created with Luna 3.4.0.0
'Author: Diego Lunadei
'Date: 18/06/2023
#End Region

Imports System
Imports System.Xml
Imports System.Xml.Serialization
Imports System.Data
Imports System.Data.SqlClient

Namespace LUNA

    Public Enum enLogicOperator
        enAND = 0
        enOR
    End Enum

    Public Class LunaContext
        Public Shared Property DateFormat() As String
            Get
                Return DbDateFormat
            End Get
            Set(ByVal value As String)
                DbDateFormat = value
            End Set
        End Property
        Public Shared Property ConnexionString() As String
            Get
                Return ChaineConnexion
            End Get
            Set(ByVal value As String)
                ChaineConnexion = value
            End Set
        End Property
        Public Shared Property Connection As Data.IDbConnection
            Get
                Return DbConnection
            End Get
            Set(value As Data.IDbConnection)
                DbConnection = value
            End Set
        End Property
        Public Shared ReadOnly Property Transaction As Data.IDbTransaction
            Get
                Return DbTransaction
            End Get
        End Property
        Public Shared Sub TransactionBegin()
            If DbTransaction Is Nothing Then
                DbTransaction = DbConnection.BeginTransaction
            End If
        End Sub
        Public Shared Sub TransactionCommit()
            If Not DbTransaction Is Nothing Then
                DbTransaction.Commit()
                DbTransaction.Dispose()
                DbTransaction = Nothing
            End If
        End Sub
        Public Shared Sub TransactionRollback()
            If Not DbTransaction Is Nothing Then
                DbTransaction.Rollback()
                DbTransaction.Dispose()
                DbTransaction = Nothing
            End If
        End Sub
        Public Shared Sub CloseDbConnection()
            Try
                If DbConnection.State <> ConnectionState.Closed Then DbConnection.Close()
                DbConnection.Dispose()
                DbConnection = Nothing
            Catch ex As Exception
            End Try
        End Sub
        Public Shared Sub OpenDbConnection(Optional ConnString As String = "")
            Try
                If DbConnection Is Nothing Then
                    Dim Connectionstring As String = String.Empty
                    If ConnString.Length Then
                        Connectionstring = ConnString
                    Else
                        Connectionstring = My.Settings("ConnectionString").ToString
                    End If
                    OpenConn(Connectionstring)
                End If
            Catch ex As Exception
            End Try
        End Sub
        Protected Shared Function OpenConn(ConnString As String) As Integer
            Dim Ris As Integer = 0
            Try
                If DbConnection Is Nothing Then DbConnection = New SqlClient.SqlConnection
                If DbConnection.ConnectionString.Length = 0 Then DbConnection.ConnectionString = ConnString
                If DbConnection.State <> Data.ConnectionState.Open Then DbConnection.Open()
            Catch ex As Exception
                Ris = 1
                Err.Raise(600, "Error Opening DB", ex.Message)
            End Try
            Return Ris
        End Function
    End Class

    Public MustInherit Class LunaBaseClass

        Public Sub ManageError(ByVal ex As Exception)
            Err.Raise(601, "ManageError", ex.Message)
        End Sub

    End Class

    Public MustInherit Class LunaBaseClassEntity

        Inherits LunaBaseClass

        Private _IsChanged As Boolean = False
        Public Property IsChanged As Boolean
            Get
                Return _IsChanged
            End Get
            Set(value As Boolean)
                _IsChanged = value
            End Set
        End Property

    End Class

    Public MustInherit Class LunaBaseClassDAO(Of T)
        Inherits LunaBaseClass
        Implements IDisposable

        Protected _cn As SqlClient.SqlConnection
        Protected _ConnectionString As String = String.Empty
        Private _ConnAdHoc As Boolean = False

        Public Sub New()

            'By default check Global Connection in context or use ConnectionString in AppSettings
            If Not DbConnection Is Nothing Then
                _cn = DbConnection
            Else
                _ConnectionString = My.Settings("ConnectionString").ToString
                OpenDBConnection()
                _ConnAdHoc = True
            End If
        End Sub

        Public Sub New(ByVal Connection As SqlClient.SqlConnection)
            _cn = Connection
            OpenDBConnection()
        End Sub

        Public Sub New(ByVal ConnectionString As String)
            If ConnectionString.Length <> 0 Then
                _ConnectionString = ConnectionString
                OpenDBConnection()
            End If
        End Sub

        Public MustOverride Function Read(ByVal Id As Integer) As T
        Public MustOverride Function Save(ByRef obj As T) As Integer
        Public MustOverride Sub Delete(ByVal Id As Integer)
        Public MustOverride Sub Delete(ByRef obj As T, Optional ByRef ListaObj As List(Of T) = Nothing)
        Public MustOverride Function Find(ByVal ParamArray Parameter() As LUNA.LunaSearchParameter) As IEnumerable(Of T)
        Public MustOverride Function GetAll(Optional ByVal OrderByField As String = "", Optional ByVal AddEmptyItem As Boolean = False) As IEnumerable(Of T)

        Protected Function OpenDBConnection() As Integer
            Dim Ris As Integer = 0
            Try
                If _cn Is Nothing Then _cn = New SqlClient.SqlConnection
                If _cn.ConnectionString.Length = 0 Then _cn.ConnectionString = _ConnectionString
                If _cn.State <> Data.ConnectionState.Open Then _cn.Open()
            Catch ex As Exception
                Ris = 1
                Err.Raise(600, "Error Opening DB", ex.Message)
            End Try
            Return Ris
        End Function

        Protected Function CloseDbConnection() As Integer
            Dim Ris As Integer = 0
            Try
                If Not _cn Is Nothing Then
                    If _cn.State = ConnectionState.Open Then
                        _cn.Close()
                    End If
                    _cn = Nothing
                End If
            Catch ex As Exception
                Ris = 1
            End Try
            Return Ris
        End Function

        Public Function Ap(ByVal Testo) As String
            Dim str As String = String.Empty
            If Not TypeOf Testo Is String Then
                str = " " & Testo.ToString
            Else
                str = Testo.ToString
                str = str.Replace("'", "''")
                str = " '" & str & "'"
            End If
            Return str
        End Function

        Public Function ApN(ByVal Testo) As String
            Dim str As String = String.Empty
            str = Testo.ToString
            str = str.Replace(",", ".")
            Return str
        End Function

        Public Function ApLike(ByVal testo)
            Dim str As String
            str = testo.ToString
            str = str.Replace(" '", "''")
            str = "like '%" & str & "%'"
            Return str
        End Function

        Public Function ApLikeRight(ByVal testo)
            Dim str As String
            str = testo.ToString
            str = str.Replace(" '", "''")
            str = "like '" & str & "%'"
            Return str
        End Function

#Region "Serialization Method"
        Public Function ReadSerialize(ByVal PathXMLSerial As String) As T

            Dim cls As T
            Try
                Dim serialize As XmlSerializer = New XmlSerializer(GetType(T))
                Dim deSerialize As IO.FileStream = New IO.FileStream(PathXMLSerial, IO.FileMode.Open)
                cls = serialize.Deserialize(deSerialize)
            Catch ex As Exception
                ManageError(ex)
            End Try

            Return cls
        End Function
        Public Sub SaveSerialize(Obj As T, ByVal PathXML As String)

            Try
                Dim serialize As XmlSerializer = New XmlSerializer(GetType(T))
                Dim Writer As New System.IO.StreamWriter(PathXML)
                serialize.Serialize(Writer, Obj)
                Writer.Close()
            Catch ex As Exception
                ManageError(ex)
            End Try

        End Sub
#End Region

#Region "IDisposable Support"
        Private disposedValue As Boolean ' To detect redundant calls
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not Me.disposedValue Then
                If _ConnAdHoc Then CloseDbConnection()
            End If
            Me.disposedValue = True
        End Sub
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
        Protected Overrides Sub Finalize()
            Dispose(False)
        End Sub
#End Region

        ' <summary>
        ' Fonction publique qui exécute un SELECT en mode déconnecté avec la connexion déjà ouverte de cet objet dans un dataSet existant.
        ' </summary>
        ' <param name="text">Requête ou nom de la procédure stockée.</param>
        ' <param name="type">Type Requête ou Procédure Stockée.</param>
        ' <param name="myDataSet">DataSet dans lequel sera placé la DataTable résultante.</param>
        ' <param name="tableName">Nom de la DataTable résultante.</param>
        ' <param name="startIndex">Index de la première ligne à retourner.</param>
        ' <param name="nbLines">Nombre de ligne à retourner</param>
        ' <returns>Un datatable contenant les données. null sinon</returns>
        Public Shared Function execSelect(ByVal text As String, ByVal type As CommandType, ByVal myDataSet As DataSet, ByVal tableName As String, ByVal startIndex As Integer, ByVal nbLines As Integer, ByVal cn As SqlConnection) As DataTable


            Dim myDataAdapter As SqlDataAdapter = New SqlDataAdapter(text, cn)

            ' Définition du type
            myDataAdapter.SelectCommand.CommandType = type
            myDataAdapter.SelectCommand.CommandTimeout = 0
            If Not DbTransaction Is Nothing Then myDataAdapter.SelectCommand.Transaction = DbTransaction

            ' Exécution du SELECT
            If (myDataSet Is Nothing) Then
                myDataSet = New DataSet()
            End If

            If (startIndex > -1 And nbLines <> 0) Then

                If (String.IsNullOrEmpty(tableName)) Then
                    tableName = "Table"
                End If
                myDataAdapter.Fill(myDataSet, startIndex, nbLines, tableName)

                If (myDataSet.Tables.IndexOf(tableName) <> -1) Then
                    Return myDataSet.Tables(tableName)
                Else
                    Return New DataTable
                End If



            ElseIf (Not String.IsNullOrEmpty(tableName)) Then

                myDataAdapter.Fill(myDataSet, tableName)

                'Retour
                If (myDataSet.Tables.IndexOf(tableName) <> -1) Then
                    Return myDataSet.Tables(tableName)
                Else
                    Return New DataTable
                End If


            Else

                myDataAdapter.Fill(myDataSet)

                'Retour
                If (myDataSet.Tables(0).Columns.Count <> 0) Then
                    Return myDataSet.Tables(0)
                Else
                    Return New DataTable
                End If

            End If
        End Function


    End Class

    Public Class LunaSearchParameter

        Public Sub New()

        End Sub

        Public Sub New(ByVal FieldName As String, ByVal Value As Object, Optional ByVal SqlOperator As String = "", Optional ByVal LogicOperator As enLogicOperator = enLogicOperator.enAND)
            _FieldName = FieldName
            _Value = Value
            If SqlOperator.Length Then _SqlOperator = SqlOperator
            _LogicOperator = LogicOperator
        End Sub

        Private _SqlOperator As String = " = "
        Public Property SqlOperator As String
            Get
                Return _SqlOperator
            End Get
            Set(ByVal value As String)
                _SqlOperator = value
            End Set
        End Property

        Private _LogicOperator As enLogicOperator = enLogicOperator.enAND
        Public Property LogicOperator As enLogicOperator
            Get
                Return _LogicOperator
            End Get
            Set(ByVal value As enLogicOperator)
                _LogicOperator = value
            End Set
        End Property

        Public ReadOnly Property LogicOperatorStr As String
            Get
                If _LogicOperator = enLogicOperator.enAND Then
                    Return " And "
                Else
                    Return " Or "
                End If
            End Get
        End Property

        Private _FieldName As String
        Public Property FieldName As String
            Get
                Return _FieldName
            End Get
            Set(ByVal value As String)
                _FieldName = value
            End Set
        End Property

        Private _Value
        Public Property Value
            Get
                Return _Value
            End Get
            Set(ByVal value)
                _Value = value
            End Set
        End Property

    End Class

End Namespace

