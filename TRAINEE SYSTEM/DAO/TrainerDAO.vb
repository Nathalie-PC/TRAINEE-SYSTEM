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

Partial Public Class Trainer
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

    Protected _department As String = ""

    <XmlElementAttribute("department")>
    Public Property department() As String
        Get
            Return _department
        End Get
        Set(ByVal value As String)
            If _department <> value Then
                IsChanged = True
                _department = value
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

    Protected _password As String = ""

    <XmlElementAttribute("password")>
    Public Property password() As String
        Get
            Return _password
        End Get
        Set(ByVal value As String)
            If _password <> value Then
                IsChanged = True
                _password = value
            End If
        End Set
    End Property
#End Region

#Region "Method"
    ''' <summary>
    '''This method read an Trainer from DB.
    ''' </summary>
    ''' <returns>
    '''Return 0 if all ok, 1 if error
    ''' </returns>
    Public Overridable Function Read(Id As Integer) As Integer
        'Return 0 if all ok
        Dim Ris As Integer = 0
        Try
            Dim Mgr As New TrainerDAO
            Dim int As Trainer = Mgr.Read(Id)
            _Id = int.Id
            _name = int.name
            _surname = int.surname
            _department = int.department
            _gender = int.gender
            _Email = int.Email
            _telephone = int.telephone
            _login = int.login
            _password = int.password
            Mgr.Dispose()
        Catch ex As Exception
            ManageError(ex)
            Ris = 1
        End Try
        Return Ris
    End Function

    ''' <summary>
    '''This method save an Trainer on DB.
    ''' </summary>
    ''' <returns>
    '''Return Id insert in DB if all ok, 0 if error
    ''' </returns>
    Public Overridable Function Save() As Integer
        'Return the id Inserted
        Dim Ris As Integer = 0
        Try
            Dim Mgr As New TrainerDAO
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
        If _department.Length > 50 Then Ris = False
        If _gender.Length > 9 Then Ris = False
        If _Email.Length > 50 Then Ris = False
        If _login.Length > 50 Then Ris = False
        If _password.Length > 50 Then Ris = False
        Return Ris
    End Function

#End Region

#Region "Embedded Class"

#End Region

End Class

''' <summary>
'''This class manage persistency on db of Trainer object
''' </summary>
''' <remarks>
'''
''' </remarks>
Partial Public Class TrainerDAO
    Inherits LUNA.LunaBaseClassDAO(Of Trainer)

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
    '''Read from DB table Trainer
    ''' </summary>
    ''' <returns>
    '''Return an Trainer object
    ''' </returns>
    Public Overrides Function Read(Id As Integer) As Trainer
        Dim cls As New Trainer

        Try
            Dim myCommand As SqlCommand = _cn.CreateCommand()
            myCommand.CommandText = "SELECT * FROM Trainer where Id = " & Id
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
                If Not myReader("department") Is DBNull.Value Then
                    cls.department = myReader("department")
                End If
                If Not myReader("gender") Is DBNull.Value Then
                    cls.gender = myReader("gender")
                End If
                If Not myReader("Email") Is DBNull.Value Then
                    cls.Email = myReader("Email")
                End If
                If Not myReader("telephone") Is DBNull.Value Then
                    cls.telephone = myReader("telephone")
                End If
                If Not myReader("login") Is DBNull.Value Then
                    cls.login = myReader("login")
                End If
                If Not myReader("password") Is DBNull.Value Then
                    cls.password = myReader("password")
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
    '''Save on DB table Trainer
    ''' </summary>
    ''' <returns>
    '''Return ID insert in DB
    ''' </returns>
    Public Overrides Function Save(ByRef cls As Trainer) As Integer

        Dim Ris As Integer = 0 'in Ris return Insert Id

        If cls.login Then
            If cls.IsChanged Then
                Dim DbCommand As SqlCommand = New SqlCommand()
                Try
                    Dim sql As String
                    DbCommand.Connection = _cn
                    If Not DbTransaction Is Nothing Then DbCommand.Transaction = DbTransaction
                    If cls.Id = 0 Then
                        sql = "INSERT INTO Trainer ("
                        sql &= "name,"
                        sql &= "surname,"
                        sql &= "department,"
                        sql &= "gender,"
                        sql &= "Email,"
                        sql &= "telephone,"
                        sql &= "login,"
                        sql &= "password"
                        sql &= ") VALUES ("
                        sql &= "@name,"
                        sql &= "@surname,"
                        sql &= "@department,"
                        sql &= "@gender,"
                        sql &= "@Email,"
                        sql &= "@telephone,"
                        sql &= "@login,"
                        sql &= "@password"
                        sql &= ")"
                    Else
                        sql = "UPDATE Trainer SET "
                        sql &= "name = @name,"
                        sql &= "surname = @surname,"
                        sql &= "department = @department,"
                        sql &= "gender = @gender,"
                        sql &= "Email = @Email,"
                        sql &= "telephone = @telephone,"
                        sql &= "login = @login,"
                        sql &= "password = @password"
                        sql &= " WHERE Id= " & cls.Id
                    End If
                    DbCommand.Parameters.Add(New SqlClient.SqlParameter("@name", cls.name))
                    DbCommand.Parameters.Add(New SqlClient.SqlParameter("@surname", cls.surname))
                    DbCommand.Parameters.Add(New SqlClient.SqlParameter("@department", cls.department))
                    DbCommand.Parameters.Add(New SqlClient.SqlParameter("@gender", cls.gender))
                    DbCommand.Parameters.Add(New SqlClient.SqlParameter("@Email", cls.Email))
                    DbCommand.Parameters.Add(New SqlClient.SqlParameter("@telephone", cls.telephone))
                    DbCommand.Parameters.Add(New SqlClient.SqlParameter("@login", cls.login))
                    DbCommand.Parameters.Add(New SqlClient.SqlParameter("@password", cls.password))
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
            'Dim Sql As String = "UPDATE Trainer SET DELETED=True "
            Dim Sql As String = "DELETE FROM Trainer"
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
    '''Delete from DB table Trainer. Accept id of object to delete.
    ''' </summary>
    ''' <returns>
    '''
    ''' </returns>
    Public Overrides Sub Delete(Id As Integer)

        DestroyPermanently(Id)

    End Sub

    ''' <summary>
    '''Delete from DB table Trainer. Accept object to delete and optional a List to remove the object from.
    ''' </summary>
    ''' <returns>
    '''
    ''' </returns>
    Public Overrides Sub Delete(ByRef obj As Trainer, Optional ByRef ListaObj As List(Of Trainer) = Nothing)

        DestroyPermanently(obj.Id)
        If Not ListaObj Is Nothing Then ListaObj.Remove(obj)

    End Sub

    Public Overloads Function Find(ByVal OrderBy As String, ByVal ParamArray Parameter() As LUNA.LunaSearchParameter) As IEnumerable(Of Trainer)
        Return FindReal(0, OrderBy, Parameter)
    End Function

    Public Overloads Function Find(ByVal Top As Integer, ByVal OrderBy As String, ByVal ParamArray Parameter() As LUNA.LunaSearchParameter) As IEnumerable(Of Trainer)
        Return FindReal(Top, OrderBy, Parameter)
    End Function

    Public Overrides Function Find(ByVal ParamArray Parameter() As LUNA.LunaSearchParameter) As IEnumerable(Of Trainer)
        Return FindReal(0, "", Parameter)
    End Function

    Private Function FindReal(ByVal Top As Integer, ByVal OrderBy As String, ByVal ParamArray Parameter() As LUNA.LunaSearchParameter) As IEnumerable(Of Trainer)
        Dim Ls As New List(Of Trainer)
        Try

            Dim sql As String = ""
sql ="SELECT " & IIf(Top, Top, "") & " Id," & _
	"name," & _
	"surname," & _
	"department," & _
	"gender," & _
	"Email," & _
	"telephone," & _
	"login," & _
	"password"
sql &=" from Trainer" 
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

Public Overrides Function GetAll(Optional OrderByField as string = "", Optional ByVal AddEmptyItem As Boolean = False) as iEnumerable(Of Trainer)
Dim Ls As New List(Of Trainer)
Try

            Dim sql As String = ""
            sql = "SELECT Id," &
    "name," &
    "surname," &
    "department," &
    "gender," &
    "Email," &
    "telephone," &
    "login," &
    "password"
            sql &= " from Trainer"
            If OrderByField.Length Then
                sql &= " ORDER BY " & OrderByField
            End If

            Ls = GetData(sql, AddEmptyItem)

        Catch ex As Exception
            ManageError(ex)
        End Try
        Return Ls
    End Function
    Private Function GetData(sql As String, Optional ByVal AddEmptyItem As Boolean = False) As IEnumerable(Of Trainer)
        Dim Ls As New List(Of Trainer)
        Try
            Dim myCommand As SqlCommand = _cn.CreateCommand()
            myCommand.CommandText = sql
            If Not DbTransaction Is Nothing Then myCommand.Transaction = DbTransaction
            Dim myReader As SqlDataReader = myCommand.ExecuteReader()
            If AddEmptyItem Then Ls.Add(New Trainer() With {.Id = 0, .name = "", .surname = "", .department = "", .gender = "", .Email = "", .telephone = 0, .login = "", .password = ""})
            While myReader.Read
                Dim classe As New Trainer
                If Not myReader("Id") Is DBNull.Value Then classe.Id = myReader("Id")
                If Not myReader("name") Is DBNull.Value Then classe.name = myReader("name")
                If Not myReader("surname") Is DBNull.Value Then classe.surname = myReader("surname")
                If Not myReader("department") Is DBNull.Value Then classe.department = myReader("department")
                If Not myReader("gender") Is DBNull.Value Then classe.gender = myReader("gender")
                If Not myReader("Email") Is DBNull.Value Then classe.Email = myReader("Email")
                If Not myReader("telephone") Is DBNull.Value Then classe.telephone = myReader("telephone")
                If Not myReader("login") Is DBNull.Value Then classe.login = myReader("login")
                If Not myReader("password") Is DBNull.Value Then classe.password = myReader("password")
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


