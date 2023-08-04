#Region "Author"
'Class created with Luna 3.4.6.11
'Author: Diego Lunadei
'Date: 2023-08-03
#End Region

Imports System
Imports System.Xml
Imports System.Xml.Serialization
Imports System.Data
Imports System.Data.SqlClient

Partial Public Class Task
    Inherits LUNA.LunaBaseClassEntity
    '******IMPORTANT: Don't write your code here. Write your code in the Class object that use this Partial Class.
    '******So you can replace DAOClass and EntityClass without lost your code

    Public Sub New()

    End Sub

#Region "Database Field Map"

    Protected _code As String = ""

    <XmlElementAttribute("code")>
    Public Property code() As String
        Get
            Return _code
        End Get
        Set(ByVal value As String)
            If _code <> value Then
                IsChanged = True
                _code = value
            End If
        End Set
    End Property

    Protected _wording As String = ""

    <XmlElementAttribute("wording")>
    Public Property wording() As String
        Get
            Return _wording
        End Get
        Set(ByVal value As String)
            If _wording <> value Then
                IsChanged = True
                _wording = value
            End If
        End Set
    End Property
#End Region

#Region "Method"
    ''' <summary>
    '''This method read an Task from DB.
    ''' </summary>
    ''' <returns>
    '''Return 0 if all ok, 1 if error
    ''' </returns>
    Public Overridable Function Read(Id As Integer) As Integer
        'Return 0 if all ok
        Dim Ris As Integer = 0
        Try
            Dim Mgr As New TaskDAO
            Dim int As Task = Mgr.Read(Id)
            _code = int.code
            _wording = int.wording
            Mgr.Dispose()
        Catch ex As Exception
            ManageError(ex)
            Ris = 1
        End Try
        Return Ris
    End Function

    ''' <summary>
    '''This method save an Task on DB.
    ''' </summary>
    ''' <returns>
    '''Return Id insert in DB if all ok, 0 if error
    ''' </returns>
    Public Overridable Function Save() As Integer
        'Return the id Inserted
        Dim Ris As Integer = 0
        Try
            Dim Mgr As New TaskDAO
            Ris = Mgr.Save(Me)
            Mgr.Dispose()
        Catch ex As Exception
            ManageError(ex)
        End Try
        Return Ris
    End Function

    Private Function InternalIsValid() As Boolean
        Dim Ris As Boolean = True
        If _code.Length = 0 Then Ris = False
        If _code.Length > 50 Then Ris = False
        If _wording.Length > 50 Then Ris = False
        Return Ris
    End Function

#End Region

#Region "Embedded Class"

#End Region

End Class

''' <summary>
'''This class manage persistency on db of Task object
''' </summary>
''' <remarks>
'''
''' </remarks>
Partial Public Class TaskDAO
    Inherits LUNA.LunaBaseClassDAO(Of Task)

    ''' <summary>
    '''New() create an istance of this class. Use default DB Connection
    ''' </summary>
    ''' <returns>
    '''
    ''' </returns>
    Public Sub New()
        MyBase.New()
    End Sub

    ''' <summary>
    '''New() create an istance of this class and specify a DB connection
    ''' </summary>
    ''' <returns>
    '''
    ''' </returns>
    Public Sub New(ByVal Connection As Data.SqlClient.SqlConnection)
        MyBase.New(Connection)
    End Sub

    ''' <summary>
    '''New() create an istance of this class and specify a DB connectionstring
    ''' </summary>
    ''' <returns>
    '''
    ''' </returns>
    Public Sub New(ByVal ConnectionString As String)
        MyBase.New(ConnectionString)
    End Sub

    ''' <summary>
    '''Read from DB table Task
    ''' </summary>
    ''' <returns>
    '''Return an Task object
    ''' </returns>
    Public Overrides Function Read(Id As Integer) As Task
        Dim cls As New Task

        Try
            Dim myCommand As SqlCommand = _cn.CreateCommand()
            myCommand.CommandText = "SELECT * FROM Task where code = " & Id
            If Not DbTransaction Is Nothing Then myCommand.Transaction = DbTransaction
            Dim myReader As SqlDataReader = myCommand.ExecuteReader()
            myReader.Read()
            If myReader.HasRows Then
                cls.code = myReader("code")
                If Not myReader("wording") Is DBNull.Value Then
                    cls.wording = myReader("wording")
                End If
            End If
            myReader.Close()
            myCommand.Dispose()

        Catch ex As Exception
            ManageError(ex)
        End Try
        Return cls
    End Function

    ''' <summary>
    '''Save on DB table Task
    ''' </summary>
    ''' <returns>
    '''Return ID insert in DB
    ''' </returns>
    Public Overrides Function Save(ByRef cls As Task) As Integer

        Dim Ris As Integer = 0 'in Ris return Insert Id

        If cls.IsChanged Then
            If cls.IsChanged Then
                Dim DbCommand As SqlCommand = New SqlCommand()
                Try
                    Dim sql As String
                    DbCommand.Connection = _cn
                    If Not DbTransaction Is Nothing Then DbCommand.Transaction = DbTransaction
                    If cls.code = 0 Then
                        sql = "INSERT INTO Task ("
                        sql &= "code,"
                        sql &= "wording"
                        sql &= ") VALUES ("
                        sql &= "@code,"
                        sql &= "@wording"
                        sql &= ")"
                    Else
                        sql = "UPDATE Task SET "
                        sql &= "code = @code,"
                        sql &= "wording = @wording"
                        sql &= " WHERE code= " & cls.code
                    End If
                    DbCommand.Parameters.Add(New SqlClient.SqlParameter("@code", cls.code))
                    DbCommand.Parameters.Add(New SqlClient.SqlParameter("@wording", cls.wording))
                    DbCommand.CommandType = CommandType.Text
                    DbCommand.CommandText = sql
                    DbCommand.ExecuteNonQuery()

                    Ris = cls.code
                    DbCommand.Dispose()

                Catch ex As Exception
                    ManageError(ex)
                End Try
            Else
                Ris = cls.code
            End If

        Else
            Err.Raise(602, "Object data is not valid")
        End If
        Return Ris
    End Function

    Private Sub DestroyPermanently(Id As Integer)
        Try

            Dim UpdateCommand As SqlCommand = New SqlCommand()
            UpdateCommand.Connection = _cn

            '******IMPORTANT: You can use this commented instruction to make a logical delete .
            '******Replace DELETED Field with your logic deleted field name.
            'Dim Sql As String = "UPDATE Task SET DELETED=True "
            Dim Sql As String = "DELETE FROM Task"
            Sql &= " Where code = " & Id

            UpdateCommand.CommandText = Sql
            If Not DbTransaction Is Nothing Then UpdateCommand.Transaction = DbTransaction
            UpdateCommand.ExecuteNonQuery()
            UpdateCommand.Dispose()
        Catch ex As Exception
            ManageError(ex)
        End Try
    End Sub

    ''' <summary>
    '''Delete from DB table Task. Accept id of object to delete.
    ''' </summary>
    ''' <returns>
    '''
    ''' </returns>
    Public Overrides Sub Delete(Id As Integer)

        DestroyPermanently(Id)

    End Sub

    ''' <summary>
    '''Delete from DB table Task. Accept object to delete and optional a List to remove the object from.
    ''' </summary>
    ''' <returns>
    '''
    ''' </returns>
    Public Overrides Sub Delete(ByRef obj As Task, Optional ByRef ListaObj As List(Of Task) = Nothing)

        DestroyPermanently(obj.code)
        If Not ListaObj Is Nothing Then ListaObj.Remove(obj)

    End Sub

    Public Overloads Function Find(ByVal OrderBy As String, ByVal ParamArray Parameter() As LUNA.LunaSearchParameter) As IEnumerable(Of Task)
        Return FindReal(0, OrderBy, Parameter)
    End Function

    Public Overloads Function Find(ByVal Top As Integer, ByVal OrderBy As String, ByVal ParamArray Parameter() As LUNA.LunaSearchParameter) As IEnumerable(Of Task)
        Return FindReal(Top, OrderBy, Parameter)
    End Function

    Public Overrides Function Find(ByVal ParamArray Parameter() As LUNA.LunaSearchParameter) As IEnumerable(Of Task)
        Return FindReal(0, "", Parameter)
    End Function

    Private Function FindReal(ByVal Top As Integer, ByVal OrderBy As String, ByVal ParamArray Parameter() As LUNA.LunaSearchParameter) As IEnumerable(Of Task)
        Dim Ls As New List(Of Task)
        Try

            Dim sql As String = ""
sql ="SELECT " & IIf(Top, Top, "") & " code," & _
	"wording"
sql &=" from Task" 
For Each Par As LUNA.LunaSearchParameter In Parameter
	If Not Par Is Nothing Then
		If Sql.IndexOf("WHERE") = -1 Then Sql &= " WHERE " Else Sql &=  " " & Par.LogicOperatorStr & " "
		Sql &= Par.FieldName & " " & Par.SqlOperator & " " & Ap(Par.Value)
	End if
Next

If OrderBy.Length Then Sql &= " ORDER BY " & OrderBy

Ls = GetData(Sql)

Catch ex As Exception
	ManageError(ex)
End Try
Return Ls
End Function

Public Overrides Function GetAll(Optional OrderByField as string = "", Optional ByVal AddEmptyItem As Boolean = False) as iEnumerable(Of Task)
Dim Ls As New List(Of Task)
Try

            Dim sql As String = ""
            sql = "SELECT code," &
    "wording"
            sql &= " from Task"
            If OrderByField.Length Then
                sql &= " ORDER BY " & OrderByField
            End If

            Ls = GetData(sql, AddEmptyItem)

        Catch ex As Exception
            ManageError(ex)
        End Try
        Return Ls
    End Function
    Private Function GetData(sql As String, Optional ByVal AddEmptyItem As Boolean = False) As IEnumerable(Of Task)
        Dim Ls As New List(Of Task)
        Try
            Dim myCommand As SqlCommand = _cn.CreateCommand()
            myCommand.CommandText = sql
            If Not DbTransaction Is Nothing Then myCommand.Transaction = DbTransaction
            Dim myReader As SqlDataReader = myCommand.ExecuteReader()
            If AddEmptyItem Then Ls.Add(New Task() With {.code = "", .wording = ""})
            While myReader.Read
                Dim classe As New Task
                If Not myReader("code") Is DBNull.Value Then classe.code = myReader("code")
                If Not myReader("wording") Is DBNull.Value Then classe.wording = myReader("wording")
                Ls.Add(classe)
            End While
            myReader.Close()
            myCommand.Dispose()

        Catch ex As Exception
            ManageError(ex)
        End Try
        Return Ls
    End Function
End Class


