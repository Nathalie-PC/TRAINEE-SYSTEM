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
Imports System.Data.OleDb

Partial Public Class Trainee
    Inherits LUNA.LunaBaseClassEntity
    '******IMPORTANT: Don't write your code here. Write your code in the Class object that use this Partial Class.
    '******So you can replace DAOClass and EntityClass without lost your code

    Public Sub New()

    End Sub

#Region "Database Field Map"

    Protected _Id As Integer = 0

    <XmlElementAttribute("Id")>
    Public Property Id() As Integer
        Get
            Return _Id
        End Get
        Set(ByVal value As Integer)
            If _Id <> value Then
                IsChanged = True
                _Id = value
            End If
        End Set
    End Property

    Protected _name As String = ""

    <XmlElementAttribute("name")>
    Public Property name() As String
        Get
            Return _name
        End Get
        Set(ByVal value As String)
            If _name <> value Then
                IsChanged = True
                _name = value
            End If
        End Set
    End Property

    Protected _surname As String = ""

    <XmlElementAttribute("surname")>
    Public Property surname() As String
        Get
            Return _surname
        End Get
        Set(ByVal value As String)
            If _surname <> value Then
                IsChanged = True
                _surname = value
            End If
        End Set
    End Property

    Protected _address As String = ""

    <XmlElementAttribute("address")>
    Public Property address() As String
        Get
            Return _address
        End Get
        Set(ByVal value As String)
            If _address <> value Then
                IsChanged = True
                _address = value
            End If
        End Set
    End Property

    Protected _Email As String = ""

    <XmlElementAttribute("Email")>
    Public Property Email() As String
        Get
            Return _Email
        End Get
        Set(ByVal value As String)
            If _Email <> value Then
                IsChanged = True
                _Email = value
            End If
        End Set
    End Property

    Protected _telephone As Integer = 0

    <XmlElementAttribute("telephone")>
    Public Property telephone() As Integer
        Get
            Return _telephone
        End Get
        Set(ByVal value As Integer)
            If _telephone <> value Then
                IsChanged = True
                _telephone = value
            End If
        End Set
    End Property

    Protected _statut As String = ""

    <XmlElementAttribute("statut")>
    Public Property statut() As String
        Get
            Return _statut
        End Get
        Set(ByVal value As String)
            If _statut <> value Then
                IsChanged = True
                _statut = value
            End If
        End Set
    End Property

    Protected _BeginningDate As DateTime = Nothing

    <XmlElementAttribute("BeginningDate")>
    Public Property BeginningDate() As DateTime
        Get
            Return _BeginningDate
        End Get
        Set(ByVal value As DateTime)
            If _BeginningDate <> value Then
                IsChanged = True
                _BeginningDate = value
            End If
        End Set
    End Property

    Protected _EndingDate As DateTime = Nothing

    <XmlElementAttribute("EndingDate")>
    Public Property EndingDate() As DateTime
        Get
            Return _EndingDate
        End Get
        Set(ByVal value As DateTime)
            If _EndingDate <> value Then
                IsChanged = True
                _EndingDate = value
            End If
        End Set
    End Property

    Protected _Department As String = ""

    <XmlElementAttribute("Department")>
    Public Property Department() As String
        Get
            Return _Department
        End Get
        Set(ByVal value As String)
            If _Department <> value Then
                IsChanged = True
                _Department = value
            End If
        End Set
    End Property

    Protected _Password As String = ""

    <XmlElementAttribute("Password")>
    Public Property Password() As String
        Get
            Return _Password
        End Get
        Set(ByVal value As String)
            If _Password <> value Then
                IsChanged = True
                _Password = value
            End If
        End Set
    End Property

    Protected _login As String = ""

    <XmlElementAttribute("login")>
    Public Property login() As String
        Get
            Return _login
        End Get
        Set(ByVal value As String)
            If _login <> value Then
                IsChanged = True
                _login = value
            End If
        End Set
    End Property

    Protected _Task As String = ""

    <XmlElementAttribute("Task")>
    Public Property Task() As String
        Get
            Return _Task
        End Get
        Set(ByVal value As String)
            If _Task <> value Then
                IsChanged = True
                _Task = value
            End If
        End Set
    End Property

    Protected _gender As String = ""

    <XmlElementAttribute("gender")>
    Public Property gender() As String
        Get
            Return _gender
        End Get
        Set(ByVal value As String)
            If _gender <> value Then
                IsChanged = True
                _gender = value
            End If
        End Set
    End Property
#End Region

#Region "Method"
    ''' <summary>
    '''This method read an Trainee from DB.
    ''' </summary>
    ''' <returns>
    '''Return 0 if all ok, 1 if error
    ''' </returns>
    Public Overridable Function Read(Id As Integer) As Integer
        'Return 0 if all ok
        Dim Ris As Integer = 0
        Try
            Dim Mgr As New TraineeDAO
            Dim int As Trainee = Mgr.Read(Id)
            _Id = int.Id
            _name = int.name
            _surname = int.surname
            _address = int.address
            _Email = int.Email
            _telephone = int.telephone
            _statut = int.statut
            _BeginningDate = int.BeginningDate
            _EndingDate = int.EndingDate
            _Department = int.Department
            _Password = int.Password
            _login = int.login
            _Task = int.Task
            _gender = int.gender
            Mgr.Dispose()
        Catch ex As Exception
            ManageError(ex)
            Ris = 1
        End Try
        Return Ris
    End Function

    ''' <summary>
    '''This method save an Trainee on DB.
    ''' </summary>
    ''' <returns>
    '''Return Id insert in DB if all ok, 0 if error
    ''' </returns>
    Public Overridable Function Save() As Integer
        'Return the id Inserted
        Dim Ris As Integer = 0
        Try
            Dim Mgr As New TraineeDAO
            Ris = Mgr.Save(Me)
            Mgr.Dispose()
        Catch ex As Exception
            ManageError(ex)
        End Try
        Return Ris
    End Function

    Private Function InternalIsValid() As Boolean
        Dim Ris As Boolean = True
        If _name.Length > 50 Then Ris = False
        If _surname.Length > 50 Then Ris = False
        If _address.Length > 50 Then Ris = False
        If _Email.Length > 50 Then Ris = False
        If _statut.Length > 50 Then Ris = False
        If _Department.Length > 50 Then Ris = False
        If _Password.Length > 50 Then Ris = False
        If _login.Length > 50 Then Ris = False
        If _Task.Length > 50 Then Ris = False
        If _gender.Length > 9 Then Ris = False
        Return Ris
    End Function

#End Region

#Region "Embedded Class"

#End Region

End Class

''' <summary>
'''This class manage persistency on db of Trainee object
''' </summary>
''' <remarks>
'''
''' </remarks>
Partial Public Class TraineeDAO
    Inherits LUNA.LunaBaseClassDAO(Of Trainee)

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
    '''Read from DB table Trainee
    ''' </summary>
    ''' <returns>
    '''Return an Trainee object
    ''' </returns>
    Public Overrides Function Read(Id As Integer) As Trainee
        Dim cls As New Trainee

        Try
            Dim myCommand As SqlCommand = _cn.CreateCommand()
            myCommand.CommandText = "SELECT * FROM Trainee where Id = " & Id
            If Not DbTransaction Is Nothing Then myCommand.Transaction = DbTransaction
            Dim myReader As SqlDataReader = myCommand.ExecuteReader()
            myReader.Read()
            If myReader.HasRows Then
                cls.Id = myReader("Id")
                If Not myReader("name") Is DBNull.Value Then
                    cls.name = myReader("name")
                End If
                If Not myReader("surname") Is DBNull.Value Then
                    cls.surname = myReader("surname")
                End If
                If Not myReader("address") Is DBNull.Value Then
                    cls.address = myReader("address")
                End If
                If Not myReader("Email") Is DBNull.Value Then
                    cls.Email = myReader("Email")
                End If
                If Not myReader("telephone") Is DBNull.Value Then
                    cls.telephone = myReader("telephone")
                End If
                If Not myReader("statut") Is DBNull.Value Then
                    cls.statut = myReader("statut")
                End If
                If Not myReader("BeginningDate") Is DBNull.Value Then
                    cls.BeginningDate = myReader("BeginningDate")
                End If
                If Not myReader("EndingDate") Is DBNull.Value Then
                    cls.EndingDate = myReader("EndingDate")
                End If
                If Not myReader("Department") Is DBNull.Value Then
                    cls.Department = myReader("Department")
                End If
                If Not myReader("Password") Is DBNull.Value Then
                    cls.Password = myReader("Password")
                End If
                If Not myReader("login") Is DBNull.Value Then
                    cls.login = myReader("login")
                End If
                If Not myReader("Task") Is DBNull.Value Then
                    cls.Task = myReader("Task")
                End If
                If Not myReader("gender") Is DBNull.Value Then
                    cls.gender = myReader("gender")
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
    '''Save on DB table Trainee
    ''' </summary>
    ''' <returns>
    '''Return ID insert in DB
    ''' </returns>
    Public Overrides Function Save(ByRef cls As Trainee) As Integer

        Dim Ris As Integer = 0 'in Ris return Insert Id

        If cls.login Then
            If cls.IsChanged Then
                Dim DbCommand As SqlCommand = New SqlCommand()
                Try
                    Dim sql As String
                    DbCommand.Connection = _cn
                    If Not DbTransaction Is Nothing Then DbCommand.Transaction = DbTransaction
                    If cls.Id = 0 Then
                        sql = "INSERT INTO Trainee ("
                        sql &= "name,"
                        sql &= "surname,"
                        sql &= "address,"
                        sql &= "Email,"
                        sql &= "telephone,"
                        sql &= "statut,"
                        sql &= "BeginningDate,"
                        sql &= "EndingDate,"
                        sql &= "Department,"
                        sql &= "Password,"
                        sql &= "login,"
                        sql &= "Task,"
                        sql &= "gender"
                        sql &= ") VALUES ("
                        sql &= "@name,"
                        sql &= "@surname,"
                        sql &= "@address,"
                        sql &= "@Email,"
                        sql &= "@telephone,"
                        sql &= "@statut,"
                        sql &= "@BeginningDate,"
                        sql &= "@EndingDate,"
                        sql &= "@Department,"
                        sql &= "@Password,"
                        sql &= "@login,"
                        sql &= "@Task,"
                        sql &= "@gender"
                        sql &= ")"
                    Else
                        sql = "UPDATE Trainee SET "
                        sql &= "name = @name,"
                        sql &= "surname = @surname,"
                        sql &= "address = @address,"
                        sql &= "Email = @Email,"
                        sql &= "telephone = @telephone,"
                        sql &= "statut = @statut,"
                        sql &= "BeginningDate = @BeginningDate,"
                        sql &= "EndingDate = @EndingDate,"
                        sql &= "Department = @Department,"
                        sql &= "Password = @Password,"
                        sql &= "login = @login,"
                        sql &= "Task = @Task,"
                        sql &= "gender = @gender"
                        sql &= " WHERE Id= " & cls.Id
                    End If
                    DbCommand.Parameters.Add(New SqlClient.SqlParameter("@name", cls.name))
                    DbCommand.Parameters.Add(New SqlClient.SqlParameter("@surname", cls.surname))
                    DbCommand.Parameters.Add(New SqlClient.SqlParameter("@address", cls.address))
                    DbCommand.Parameters.Add(New SqlClient.SqlParameter("@Email", cls.Email))
                    DbCommand.Parameters.Add(New SqlClient.SqlParameter("@telephone", cls.telephone))
                    DbCommand.Parameters.Add(New SqlClient.SqlParameter("@statut", cls.statut))
                    If cls.BeginningDate <> Date.MinValue Then
                        Dim DataPar As New SqlClient.SqlParameter("@BeginningDate", OleDbType.Date)
                        DataPar.Value = cls.BeginningDate
                        DbCommand.Parameters.Add(DataPar)
                    Else
                        DbCommand.Parameters.Add(New SqlClient.SqlParameter("@BeginningDate", DBNull.Value))
                    End If
                    If cls.EndingDate <> Date.MinValue Then
                        Dim DataPar As New SqlClient.SqlParameter("@EndingDate", OleDbType.Date)
                        DataPar.Value = cls.EndingDate
                        DbCommand.Parameters.Add(DataPar)
                    Else
                        DbCommand.Parameters.Add(New SqlClient.SqlParameter("@EndingDate", DBNull.Value))
                    End If
                    DbCommand.Parameters.Add(New SqlClient.SqlParameter("@Department", cls.Department))
                    DbCommand.Parameters.Add(New SqlClient.SqlParameter("@Password", cls.Password))
                    DbCommand.Parameters.Add(New SqlClient.SqlParameter("@login", cls.login))
                    DbCommand.Parameters.Add(New SqlClient.SqlParameter("@Task", cls.Task))
                    DbCommand.Parameters.Add(New SqlClient.SqlParameter("@gender", cls.gender))
                    DbCommand.CommandType = CommandType.Text
                    DbCommand.CommandText = sql
                    DbCommand.ExecuteNonQuery()

                    If cls.Id = 0 Then
                        Dim IdInserito As Integer = 0
                        sql = "select @@identity"
                        DbCommand.CommandText = sql
                        IdInserito = DbCommand.ExecuteScalar()
                        cls.Id = IdInserito
                        Ris = IdInserito
                    Else
                        Ris = cls.Id
                    End If

                    DbCommand.Dispose()

                Catch ex As Exception
                    ManageError(ex)
                End Try
            Else
                Ris = cls.Id
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
            'Dim Sql As String = "UPDATE Trainee SET DELETED=True "
            Dim Sql As String = "DELETE FROM Trainee"
            Sql &= " Where Id = " & Id

            UpdateCommand.CommandText = Sql
            If Not DbTransaction Is Nothing Then UpdateCommand.Transaction = DbTransaction
            UpdateCommand.ExecuteNonQuery()
            UpdateCommand.Dispose()
        Catch ex As Exception
            ManageError(ex)
        End Try
    End Sub

    ''' <summary>
    '''Delete from DB table Trainee. Accept id of object to delete.
    ''' </summary>
    ''' <returns>
    '''
    ''' </returns>
    Public Overrides Sub Delete(Id As Integer)

        DestroyPermanently(Id)

    End Sub

    ''' <summary>
    '''Delete from DB table Trainee. Accept object to delete and optional a List to remove the object from.
    ''' </summary>
    ''' <returns>
    '''
    ''' </returns>
    Public Overrides Sub Delete(ByRef obj As Trainee, Optional ByRef ListaObj As List(Of Trainee) = Nothing)

        DestroyPermanently(obj.Id)
        If Not ListaObj Is Nothing Then ListaObj.Remove(obj)

    End Sub

    Public Overloads Function Find(ByVal OrderBy As String, ByVal ParamArray Parameter() As LUNA.LunaSearchParameter) As IEnumerable(Of Trainee)
        Return FindReal(0, OrderBy, Parameter)
    End Function

    Public Overloads Function Find(ByVal Top As Integer, ByVal OrderBy As String, ByVal ParamArray Parameter() As LUNA.LunaSearchParameter) As IEnumerable(Of Trainee)
        Return FindReal(Top, OrderBy, Parameter)
    End Function

    Public Overrides Function Find(ByVal ParamArray Parameter() As LUNA.LunaSearchParameter) As IEnumerable(Of Trainee)
        Return FindReal(0, "", Parameter)
    End Function

    Private Function FindReal(ByVal Top As Integer, ByVal OrderBy As String, ByVal ParamArray Parameter() As LUNA.LunaSearchParameter) As IEnumerable(Of Trainee)
        Dim Ls As New List(Of Trainee)
        Try

            Dim sql As String = ""
sql ="SELECT " & IIf(Top, Top, "") & " Id," & _
	"name," & _
	"surname," & _
	"address," & _
	"Email," & _
	"telephone," & _
	"statut," & _
	"BeginningDate," & _
	"EndingDate," & _
	"Department," & _
	"Password," & _
	"login," & _
	"Task," & _
	"gender"
sql &=" from Trainee" 
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

Public Overrides Function GetAll(Optional OrderByField as string = "", Optional ByVal AddEmptyItem As Boolean = False) as iEnumerable(Of Trainee)
Dim Ls As New List(Of Trainee)
Try

            Dim sql As String = ""
            sql = "SELECT Id," &
    "name," &
    "surname," &
    "address," &
    "Email," &
    "telephone," &
    "statut," &
    "BeginningDate," &
    "EndingDate," &
    "Department," &
    "Password," &
    "login," &
    "Task," &
    "gender"
            sql &= " from Trainee"
            If OrderByField.Length Then
                sql &= " ORDER BY " & OrderByField
            End If

            Ls = GetData(sql, AddEmptyItem)

        Catch ex As Exception
            ManageError(ex)
        End Try
        Return Ls
    End Function
    Private Function GetData(sql As String, Optional ByVal AddEmptyItem As Boolean = False) As IEnumerable(Of Trainee)
        Dim Ls As New List(Of Trainee)
        Try
            Dim myCommand As SqlCommand = _cn.CreateCommand()
            myCommand.CommandText = sql
            If Not DbTransaction Is Nothing Then myCommand.Transaction = DbTransaction
            Dim myReader As SqlDataReader = myCommand.ExecuteReader()
            If AddEmptyItem Then Ls.Add(New Trainee() With {.Id = 0, .name = "", .surname = "", .address = "", .Email = "", .telephone = 0, .statut = "", .BeginningDate = Nothing, .EndingDate = Nothing, .Department = "", .Password = "", .login = "", .Task = "", .gender = ""})
            While myReader.Read
                Dim classe As New Trainee
                If Not myReader("Id") Is DBNull.Value Then classe.Id = myReader("Id")
                If Not myReader("name") Is DBNull.Value Then classe.name = myReader("name")
                If Not myReader("surname") Is DBNull.Value Then classe.surname = myReader("surname")
                If Not myReader("address") Is DBNull.Value Then classe.address = myReader("address")
                If Not myReader("Email") Is DBNull.Value Then classe.Email = myReader("Email")
                If Not myReader("telephone") Is DBNull.Value Then classe.telephone = myReader("telephone")
                If Not myReader("statut") Is DBNull.Value Then classe.statut = myReader("statut")
                If Not myReader("BeginningDate") Is DBNull.Value Then classe.BeginningDate = myReader("BeginningDate")
                If Not myReader("EndingDate") Is DBNull.Value Then classe.EndingDate = myReader("EndingDate")
                If Not myReader("Department") Is DBNull.Value Then classe.Department = myReader("Department")
                If Not myReader("Password") Is DBNull.Value Then classe.Password = myReader("Password")
                If Not myReader("login") Is DBNull.Value Then classe.login = myReader("login")
                If Not myReader("Task") Is DBNull.Value Then classe.Task = myReader("Task")
                If Not myReader("gender") Is DBNull.Value Then classe.gender = myReader("gender")
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


