using System;
using System.Data;
using System.Data.SqlClient;

public class studentsData
{

    public static DataTable SelectAll()
    {
        SqlConnection connection = SchoolData.GetConnection();
        string selectStatement
            = "SELECT "  
            + "     [Student ID] = [students].[StudentId] "
            + "    ,[Class ID] = [studentclass].[ClassId] "
            + "    ,[Full Name] = [students].[FullName] "
            + "    ,[Date Of Birth] = [students].[DateOfBirth] "
            + "    ,[Home Address] = [students].[HomeAddress] "
            + "    ,[Gender] = [students].[Gender] "
            + "    ,[Father] = [students].[Father] "
            + "    ,[Mother] = [students].[Mother] "
            + "    ,[Parent Contact] = [students].[ParentContact] "
            + "FROM " 
            + "     [students] " 
            + "LEFT JOIN [studentclass] ON [students].[ClassId] = [studentclass].[ClassId] "
            + "";
        SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
        selectCommand.CommandType = CommandType.Text;
        DataTable dt = new DataTable();
        try
        {
            connection.Open();
            SqlDataReader reader = selectCommand.ExecuteReader();
            if (reader.HasRows) {
                dt.Load(reader); }
            reader.Close();
        }
        catch (SqlException)
        {
            return dt;
        }
        finally
        {
            connection.Close();
        }
        return dt;
    }

    public static DataTable Search(string sField, string sCondition, string sValue)
    {
        SqlConnection connection = SchoolData.GetConnection();
        string selectStatement = "";
        if (sCondition == "Contains") {
            selectStatement
                = "SELECT "
            + "     [students].[StudentId] "
            + "    ,[studentclass].[ClassId] "
            + "    ,[students].[FullName] "
            + "    ,[students].[DateOfBirth] "
            + "    ,[students].[HomeAddress] "
            + "    ,[students].[Gender] "
            + "    ,[students].[Father] "
            + "    ,[students].[Mother] "
            + "    ,[students].[ParentContact] "
            + "FROM " 
            + "     [students] " 
            + "LEFT JOIN [studentclass] ON [students].[ClassId] = [studentclass].[ClassId] "
                + "WHERE " 
                + "     (@StudentId IS NULL OR @StudentId = '' OR [students].[StudentId] LIKE '%' + LTRIM(RTRIM(@StudentId)) + '%') " 
                + "AND   (@ClassId IS NULL OR @ClassId = '' OR [studentclass].[ClassId] LIKE '%' + LTRIM(RTRIM(@ClassId)) + '%') " 
                + "AND   (@FullName IS NULL OR @FullName = '' OR [students].[FullName] LIKE '%' + LTRIM(RTRIM(@FullName)) + '%') " 
                + "AND   (@DateOfBirth IS NULL OR @DateOfBirth = '' OR [students].[DateOfBirth] LIKE '%' + LTRIM(RTRIM(@DateOfBirth)) + '%') " 
                + "AND   (@HomeAddress IS NULL OR @HomeAddress = '' OR [students].[HomeAddress] LIKE '%' + LTRIM(RTRIM(@HomeAddress)) + '%') " 
                + "AND   (@Gender IS NULL OR @Gender = '' OR [students].[Gender] LIKE '%' + LTRIM(RTRIM(@Gender)) + '%') " 
                + "AND   (@Father IS NULL OR @Father = '' OR [students].[Father] LIKE '%' + LTRIM(RTRIM(@Father)) + '%') " 
                + "AND   (@Mother IS NULL OR @Mother = '' OR [students].[Mother] LIKE '%' + LTRIM(RTRIM(@Mother)) + '%') " 
                + "AND   (@ParentContact IS NULL OR @ParentContact = '' OR [students].[ParentContact] LIKE '%' + LTRIM(RTRIM(@ParentContact)) + '%') " 
                + "";
        } else if (sCondition == "Equals") {
            selectStatement
                = "SELECT "
            + "     [students].[StudentId] "
            + "    ,[studentclass].[ClassId] "
            + "    ,[students].[FullName] "
            + "    ,[students].[DateOfBirth] "
            + "    ,[students].[HomeAddress] "
            + "    ,[students].[Gender] "
            + "    ,[students].[Father] "
            + "    ,[students].[Mother] "
            + "    ,[students].[ParentContact] "
            + "FROM " 
            + "     [students] " 
            + "LEFT JOIN [studentclass] ON [students].[ClassId] = [studentclass].[ClassId] "
                + "WHERE " 
                + "     (@StudentId IS NULL OR @StudentId = '' OR [students].[StudentId] = LTRIM(RTRIM(@StudentId))) " 
                + "AND   (@ClassId IS NULL OR @ClassId = '' OR [studentclass].[ClassId] = LTRIM(RTRIM(@ClassId))) " 
                + "AND   (@FullName IS NULL OR @FullName = '' OR [students].[FullName] = LTRIM(RTRIM(@FullName))) " 
                + "AND   (@DateOfBirth IS NULL OR @DateOfBirth = '' OR [students].[DateOfBirth] = LTRIM(RTRIM(@DateOfBirth))) " 
                + "AND   (@HomeAddress IS NULL OR @HomeAddress = '' OR [students].[HomeAddress] = LTRIM(RTRIM(@HomeAddress))) " 
                + "AND   (@Gender IS NULL OR @Gender = '' OR [students].[Gender] = LTRIM(RTRIM(@Gender))) " 
                + "AND   (@Father IS NULL OR @Father = '' OR [students].[Father] = LTRIM(RTRIM(@Father))) " 
                + "AND   (@Mother IS NULL OR @Mother = '' OR [students].[Mother] = LTRIM(RTRIM(@Mother))) " 
                + "AND   (@ParentContact IS NULL OR @ParentContact = '' OR [students].[ParentContact] = LTRIM(RTRIM(@ParentContact))) " 
                + "";
        } else if  (sCondition == "Starts with...") {
            selectStatement
                = "SELECT "
            + "     [students].[StudentId] "
            + "    ,[studentclass].[ClassId] "
            + "    ,[students].[FullName] "
            + "    ,[students].[DateOfBirth] "
            + "    ,[students].[HomeAddress] "
            + "    ,[students].[Gender] "
            + "    ,[students].[Father] "
            + "    ,[students].[Mother] "
            + "    ,[students].[ParentContact] "
            + "FROM " 
            + "     [students] " 
            + "LEFT JOIN [studentclass] ON [students].[ClassId] = [studentclass].[ClassId] "
                + "WHERE " 
                + "     (@StudentId IS NULL OR @StudentId = '' OR [students].[StudentId] LIKE LTRIM(RTRIM(@StudentId)) + '%') " 
                + "AND   (@ClassId IS NULL OR @ClassId = '' OR [studentclass].[ClassId] LIKE LTRIM(RTRIM(@ClassId)) + '%') " 
                + "AND   (@FullName IS NULL OR @FullName = '' OR [students].[FullName] LIKE LTRIM(RTRIM(@FullName)) + '%') " 
                + "AND   (@DateOfBirth IS NULL OR @DateOfBirth = '' OR [students].[DateOfBirth] LIKE LTRIM(RTRIM(@DateOfBirth)) + '%') " 
                + "AND   (@HomeAddress IS NULL OR @HomeAddress = '' OR [students].[HomeAddress] LIKE LTRIM(RTRIM(@HomeAddress)) + '%') " 
                + "AND   (@Gender IS NULL OR @Gender = '' OR [students].[Gender] LIKE LTRIM(RTRIM(@Gender)) + '%') " 
                + "AND   (@Father IS NULL OR @Father = '' OR [students].[Father] LIKE LTRIM(RTRIM(@Father)) + '%') " 
                + "AND   (@Mother IS NULL OR @Mother = '' OR [students].[Mother] LIKE LTRIM(RTRIM(@Mother)) + '%') " 
                + "AND   (@ParentContact IS NULL OR @ParentContact = '' OR [students].[ParentContact] LIKE LTRIM(RTRIM(@ParentContact)) + '%') " 
                + "";
        } else if  (sCondition == "More than...") {
            selectStatement
                = "SELECT "
            + "     [students].[StudentId] "
            + "    ,[studentclass].[ClassId] "
            + "    ,[students].[FullName] "
            + "    ,[students].[DateOfBirth] "
            + "    ,[students].[HomeAddress] "
            + "    ,[students].[Gender] "
            + "    ,[students].[Father] "
            + "    ,[students].[Mother] "
            + "    ,[students].[ParentContact] "
            + "FROM " 
            + "     [students] " 
            + "LEFT JOIN [studentclass] ON [students].[ClassId] = [studentclass].[ClassId] "
                + "WHERE " 
                + "     (@StudentId IS NULL OR @StudentId = '' OR [students].[StudentId] > LTRIM(RTRIM(@StudentId))) " 
                + "AND   (@ClassId IS NULL OR @ClassId = '' OR [studentclass].[ClassId] > LTRIM(RTRIM(@ClassId))) " 
                + "AND   (@FullName IS NULL OR @FullName = '' OR [students].[FullName] > LTRIM(RTRIM(@FullName))) " 
                + "AND   (@DateOfBirth IS NULL OR @DateOfBirth = '' OR [students].[DateOfBirth] > LTRIM(RTRIM(@DateOfBirth))) " 
                + "AND   (@HomeAddress IS NULL OR @HomeAddress = '' OR [students].[HomeAddress] > LTRIM(RTRIM(@HomeAddress))) " 
                + "AND   (@Gender IS NULL OR @Gender = '' OR [students].[Gender] > LTRIM(RTRIM(@Gender))) " 
                + "AND   (@Father IS NULL OR @Father = '' OR [students].[Father] > LTRIM(RTRIM(@Father))) " 
                + "AND   (@Mother IS NULL OR @Mother = '' OR [students].[Mother] > LTRIM(RTRIM(@Mother))) " 
                + "AND   (@ParentContact IS NULL OR @ParentContact = '' OR [students].[ParentContact] > LTRIM(RTRIM(@ParentContact))) " 
                + "";
        } else if  (sCondition == "Less than...") {
            selectStatement
                = "SELECT " 
            + "     [students].[StudentId] "
            + "    ,[studentclass].[ClassId] "
            + "    ,[students].[FullName] "
            + "    ,[students].[DateOfBirth] "
            + "    ,[students].[HomeAddress] "
            + "    ,[students].[Gender] "
            + "    ,[students].[Father] "
            + "    ,[students].[Mother] "
            + "    ,[students].[ParentContact] "
            + "FROM " 
            + "     [students] " 
            + "LEFT JOIN [studentclass] ON [students].[ClassId] = [studentclass].[ClassId] "
                + "WHERE " 
                + "     (@StudentId IS NULL OR @StudentId = '' OR [students].[StudentId] < LTRIM(RTRIM(@StudentId))) " 
                + "AND   (@ClassId IS NULL OR @ClassId = '' OR [studentclass].[ClassId] < LTRIM(RTRIM(@ClassId))) " 
                + "AND   (@FullName IS NULL OR @FullName = '' OR [students].[FullName] < LTRIM(RTRIM(@FullName))) " 
                + "AND   (@DateOfBirth IS NULL OR @DateOfBirth = '' OR [students].[DateOfBirth] < LTRIM(RTRIM(@DateOfBirth))) " 
                + "AND   (@HomeAddress IS NULL OR @HomeAddress = '' OR [students].[HomeAddress] < LTRIM(RTRIM(@HomeAddress))) " 
                + "AND   (@Gender IS NULL OR @Gender = '' OR [students].[Gender] < LTRIM(RTRIM(@Gender))) " 
                + "AND   (@Father IS NULL OR @Father = '' OR [students].[Father] < LTRIM(RTRIM(@Father))) " 
                + "AND   (@Mother IS NULL OR @Mother = '' OR [students].[Mother] < LTRIM(RTRIM(@Mother))) " 
                + "AND   (@ParentContact IS NULL OR @ParentContact = '' OR [students].[ParentContact] < LTRIM(RTRIM(@ParentContact))) " 
                + "";
        } else if  (sCondition == "Equal or more than...") {
            selectStatement
                = "SELECT "
            + "     [students].[StudentId] "
            + "    ,[studentclass].[ClassId] "
            + "    ,[students].[FullName] "
            + "    ,[students].[DateOfBirth] "
            + "    ,[students].[HomeAddress] "
            + "    ,[students].[Gender] "
            + "    ,[students].[Father] "
            + "    ,[students].[Mother] "
            + "    ,[students].[ParentContact] "
            + "FROM " 
            + "     [students] " 
            + "LEFT JOIN [studentclass] ON [students].[ClassId] = [studentclass].[ClassId] "
                + "WHERE " 
                + "     (@StudentId IS NULL OR @StudentId = '' OR [students].[StudentId] >= LTRIM(RTRIM(@StudentId))) " 
                + "AND   (@ClassId IS NULL OR @ClassId = '' OR [studentclass].[ClassId] >= LTRIM(RTRIM(@ClassId))) " 
                + "AND   (@FullName IS NULL OR @FullName = '' OR [students].[FullName] >= LTRIM(RTRIM(@FullName))) " 
                + "AND   (@DateOfBirth IS NULL OR @DateOfBirth = '' OR [students].[DateOfBirth] >= LTRIM(RTRIM(@DateOfBirth))) " 
                + "AND   (@HomeAddress IS NULL OR @HomeAddress = '' OR [students].[HomeAddress] >= LTRIM(RTRIM(@HomeAddress))) " 
                + "AND   (@Gender IS NULL OR @Gender = '' OR [students].[Gender] >= LTRIM(RTRIM(@Gender))) " 
                + "AND   (@Father IS NULL OR @Father = '' OR [students].[Father] >= LTRIM(RTRIM(@Father))) " 
                + "AND   (@Mother IS NULL OR @Mother = '' OR [students].[Mother] >= LTRIM(RTRIM(@Mother))) " 
                + "AND   (@ParentContact IS NULL OR @ParentContact = '' OR [students].[ParentContact] >= LTRIM(RTRIM(@ParentContact))) " 
                + "";
        } else if (sCondition == "Equal or less than...") {
            selectStatement 
                = "SELECT "
            + "     [students].[StudentId] "
            + "    ,[studentclass].[ClassId] "
            + "    ,[students].[FullName] "
            + "    ,[students].[DateOfBirth] "
            + "    ,[students].[HomeAddress] "
            + "    ,[students].[Gender] "
            + "    ,[students].[Father] "
            + "    ,[students].[Mother] "
            + "    ,[students].[ParentContact] "
            + "FROM " 
            + "     [students] " 
            + "LEFT JOIN [studentclass] ON [students].[ClassId] = [studentclass].[ClassId] "
                + "WHERE " 
                + "     (@StudentId IS NULL OR @StudentId = '' OR [students].[StudentId] <= LTRIM(RTRIM(@StudentId))) " 
                + "AND   (@ClassId IS NULL OR @ClassId = '' OR [studentclass].[ClassId] <= LTRIM(RTRIM(@ClassId))) " 
                + "AND   (@FullName IS NULL OR @FullName = '' OR [students].[FullName] <= LTRIM(RTRIM(@FullName))) " 
                + "AND   (@DateOfBirth IS NULL OR @DateOfBirth = '' OR [students].[DateOfBirth] <= LTRIM(RTRIM(@DateOfBirth))) " 
                + "AND   (@HomeAddress IS NULL OR @HomeAddress = '' OR [students].[HomeAddress] <= LTRIM(RTRIM(@HomeAddress))) " 
                + "AND   (@Gender IS NULL OR @Gender = '' OR [students].[Gender] <= LTRIM(RTRIM(@Gender))) " 
                + "AND   (@Father IS NULL OR @Father = '' OR [students].[Father] <= LTRIM(RTRIM(@Father))) " 
                + "AND   (@Mother IS NULL OR @Mother = '' OR [students].[Mother] <= LTRIM(RTRIM(@Mother))) " 
                + "AND   (@ParentContact IS NULL OR @ParentContact = '' OR [students].[ParentContact] <= LTRIM(RTRIM(@ParentContact))) " 
                + "";
        }
        SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
        selectCommand.CommandType = CommandType.Text;
        if (sField == "Student ID") {
            selectCommand.Parameters.AddWithValue("@StudentId", sValue);
        } else {
            selectCommand.Parameters.AddWithValue("@StudentId", DBNull.Value); }
        if (sField == "Class ID") {
            selectCommand.Parameters.AddWithValue("@ClassId", sValue);
        } else {
            selectCommand.Parameters.AddWithValue("@ClassId", DBNull.Value); }
        if (sField == "Full Name") {
            selectCommand.Parameters.AddWithValue("@FullName", sValue);
        } else {
            selectCommand.Parameters.AddWithValue("@FullName", DBNull.Value); }
        if (sField == "Date Of Birth") {
            selectCommand.Parameters.AddWithValue("@DateOfBirth", sValue);
        } else {
            selectCommand.Parameters.AddWithValue("@DateOfBirth", DBNull.Value); }
        if (sField == "Home Address") {
            selectCommand.Parameters.AddWithValue("@HomeAddress", sValue);
        } else {
            selectCommand.Parameters.AddWithValue("@HomeAddress", DBNull.Value); }
        if (sField == "Gender") {
            selectCommand.Parameters.AddWithValue("@Gender", sValue);
        } else {
            selectCommand.Parameters.AddWithValue("@Gender", DBNull.Value); }
        if (sField == "Father") {
            selectCommand.Parameters.AddWithValue("@Father", sValue);
        } else {
            selectCommand.Parameters.AddWithValue("@Father", DBNull.Value); }
        if (sField == "Mother") {
            selectCommand.Parameters.AddWithValue("@Mother", sValue);
        } else {
            selectCommand.Parameters.AddWithValue("@Mother", DBNull.Value); }
        if (sField == "Parent Contact") {
            selectCommand.Parameters.AddWithValue("@ParentContact", sValue);
        } else {
            selectCommand.Parameters.AddWithValue("@ParentContact", DBNull.Value); }
        DataTable dt = new DataTable();
        try
        {
            connection.Open();
            SqlDataReader reader = selectCommand.ExecuteReader();
            if (reader.HasRows) {
                dt.Load(reader); }
            reader.Close();
        }
        catch (SqlException)
        {
            return dt;
        }
        finally
        {
            connection.Close();
        }
        return dt;
    }

    public static students Select_Record(students clsstudentsPara)
    {
        students clsstudents = new students();
        SqlConnection connection = SchoolData.GetConnection();
        string selectStatement
            = "SELECT " 
            + "     [StudentId] "
            + "    ,[ClassId] "
            + "    ,[FullName] "
            + "    ,[DateOfBirth] "
            + "    ,[HomeAddress] "
            + "    ,[Gender] "
            + "    ,[Father] "
            + "    ,[Mother] "
            + "    ,[ParentContact] "
            + "FROM "
            + "     [students] "
            + "WHERE "
            + "     [StudentId] = @StudentId "
            + "";
        SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
        selectCommand.CommandType = CommandType.Text;
        selectCommand.Parameters.AddWithValue("@StudentId", clsstudentsPara.StudentId);
        try
        {
            connection.Open();
            SqlDataReader reader
                = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
                clsstudents.StudentId = System.Convert.ToInt32(reader["StudentId"]);
                clsstudents.ClassId = reader["ClassId"] is DBNull ? null : (Int32?)reader["ClassId"];
                clsstudents.FullName = reader["FullName"] is DBNull ? null : reader["FullName"].ToString();
                clsstudents.DateOfBirth = reader["DateOfBirth"] is DBNull ? null : (DateTime?)reader["DateOfBirth"];
                clsstudents.HomeAddress = reader["HomeAddress"] is DBNull ? null : reader["HomeAddress"].ToString();
                clsstudents.Gender = System.Convert.ToString(reader["Gender"]);
                clsstudents.Father = reader["Father"] is DBNull ? null : reader["Father"].ToString();
                clsstudents.Mother = reader["Mother"] is DBNull ? null : reader["Mother"].ToString();
                clsstudents.ParentContact = reader["ParentContact"] is DBNull ? null : reader["ParentContact"].ToString();
            }
            else
            {
                clsstudents = null;
            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            throw ex;
        }
        finally
        {
            connection.Close();
        }
        return clsstudents;
    }

    public static bool Add(students clsstudents)
    {
        SqlConnection connection = SchoolData.GetConnection();
        string insertStatement
            = "INSERT " 
            + "     [students] "
            + "     ( "
            + "     [ClassId] "
            + "    ,[FullName] "
            + "    ,[DateOfBirth] "
            + "    ,[HomeAddress] "
            + "    ,[Gender] "
            + "    ,[Father] "
            + "    ,[Mother] "
            + "    ,[ParentContact] "
            + "     ) "
            + "VALUES " 
            + "     ( "
            + "     @ClassId "
            + "    ,@FullName "
            + "    ,@DateOfBirth "
            + "    ,@HomeAddress "
            + "    ,@Gender "
            + "    ,@Father "
            + "    ,@Mother "
            + "    ,@ParentContact "
            + "     ) "
            + "";
        SqlCommand insertCommand = new SqlCommand(insertStatement, connection);
        insertCommand.CommandType = CommandType.Text;
        if (clsstudents.ClassId.HasValue == true) {
            insertCommand.Parameters.AddWithValue("@ClassId", clsstudents.ClassId);
        } else {
            insertCommand.Parameters.AddWithValue("@ClassId", DBNull.Value); }
        if (clsstudents.FullName != null) {
            insertCommand.Parameters.AddWithValue("@FullName", clsstudents.FullName);
        } else {
            insertCommand.Parameters.AddWithValue("@FullName", DBNull.Value); }
        if (clsstudents.DateOfBirth.HasValue == true) {
            insertCommand.Parameters.AddWithValue("@DateOfBirth", clsstudents.DateOfBirth);
        } else {
            insertCommand.Parameters.AddWithValue("@DateOfBirth", DBNull.Value); }
        if (clsstudents.HomeAddress != null) {
            insertCommand.Parameters.AddWithValue("@HomeAddress", clsstudents.HomeAddress);
        } else {
            insertCommand.Parameters.AddWithValue("@HomeAddress", DBNull.Value); }
        insertCommand.Parameters.AddWithValue("@Gender", clsstudents.Gender);
        if (clsstudents.Father != null) {
            insertCommand.Parameters.AddWithValue("@Father", clsstudents.Father);
        } else {
            insertCommand.Parameters.AddWithValue("@Father", DBNull.Value); }
        if (clsstudents.Mother != null) {
            insertCommand.Parameters.AddWithValue("@Mother", clsstudents.Mother);
        } else {
            insertCommand.Parameters.AddWithValue("@Mother", DBNull.Value); }
        if (clsstudents.ParentContact != null) {
            insertCommand.Parameters.AddWithValue("@ParentContact", clsstudents.ParentContact);
        } else {
            insertCommand.Parameters.AddWithValue("@ParentContact", DBNull.Value); }
        try
        {
            connection.Open();
            int count = insertCommand.ExecuteNonQuery();
            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (SqlException ex)
        {
            throw ex;
        }
        finally
        {
            connection.Close();
        }
    }

    public static bool Update(students oldstudents, 
           students newstudents)
    {
        SqlConnection connection = SchoolData.GetConnection();
        string updateStatement
            = "UPDATE "  
            + "     [students] "
            + "SET "
            + "     [ClassId] = @NewClassId "
            + "    ,[FullName] = @NewFullName "
            + "    ,[DateOfBirth] = @NewDateOfBirth "
            + "    ,[HomeAddress] = @NewHomeAddress "
            + "    ,[Gender] = @NewGender "
            + "    ,[Father] = @NewFather "
            + "    ,[Mother] = @NewMother "
            + "    ,[ParentContact] = @NewParentContact "
            + "WHERE "
            + "     [StudentId] = @OldStudentId " 
            + " AND ((@OldClassId IS NULL AND [ClassId] IS NULL) OR [ClassId] = @OldClassId) " 
            + " AND ((@OldFullName IS NULL AND [FullName] IS NULL) OR [FullName] = @OldFullName) " 
            + " AND ((@OldDateOfBirth IS NULL AND [DateOfBirth] IS NULL) OR [DateOfBirth] = @OldDateOfBirth) " 
            + " AND ((@OldHomeAddress IS NULL AND [HomeAddress] IS NULL) OR [HomeAddress] = @OldHomeAddress) " 
            + " AND [Gender] = @OldGender " 
            + " AND ((@OldFather IS NULL AND [Father] IS NULL) OR [Father] = @OldFather) " 
            + " AND ((@OldMother IS NULL AND [Mother] IS NULL) OR [Mother] = @OldMother) " 
            + " AND ((@OldParentContact IS NULL AND [ParentContact] IS NULL) OR [ParentContact] = @OldParentContact) " 
            + "";
        SqlCommand updateCommand = new SqlCommand(updateStatement, connection);
        updateCommand.CommandType = CommandType.Text;
        if (newstudents.ClassId.HasValue == true) {
            updateCommand.Parameters.AddWithValue("@NewClassId", newstudents.ClassId);
        } else {
            updateCommand.Parameters.AddWithValue("@NewClassId", DBNull.Value); }
        if (newstudents.FullName != null) {
            updateCommand.Parameters.AddWithValue("@NewFullName", newstudents.FullName);
        } else {
            updateCommand.Parameters.AddWithValue("@NewFullName", DBNull.Value); }
        if (newstudents.DateOfBirth.HasValue == true) {
            updateCommand.Parameters.AddWithValue("@NewDateOfBirth", newstudents.DateOfBirth);
        } else {
            updateCommand.Parameters.AddWithValue("@NewDateOfBirth", DBNull.Value); }
        if (newstudents.HomeAddress != null) {
            updateCommand.Parameters.AddWithValue("@NewHomeAddress", newstudents.HomeAddress);
        } else {
            updateCommand.Parameters.AddWithValue("@NewHomeAddress", DBNull.Value); }
        updateCommand.Parameters.AddWithValue("@NewGender", newstudents.Gender);
        if (newstudents.Father != null) {
            updateCommand.Parameters.AddWithValue("@NewFather", newstudents.Father);
        } else {
            updateCommand.Parameters.AddWithValue("@NewFather", DBNull.Value); }
        if (newstudents.Mother != null) {
            updateCommand.Parameters.AddWithValue("@NewMother", newstudents.Mother);
        } else {
            updateCommand.Parameters.AddWithValue("@NewMother", DBNull.Value); }
        if (newstudents.ParentContact != null) {
            updateCommand.Parameters.AddWithValue("@NewParentContact", newstudents.ParentContact);
        } else {
            updateCommand.Parameters.AddWithValue("@NewParentContact", DBNull.Value); }
        updateCommand.Parameters.AddWithValue("@OldStudentId", oldstudents.StudentId);
        if (oldstudents.ClassId.HasValue == true) {
            updateCommand.Parameters.AddWithValue("@OldClassId", oldstudents.ClassId);
        } else {
            updateCommand.Parameters.AddWithValue("@OldClassId", DBNull.Value); }
        if (oldstudents.FullName != null) {
            updateCommand.Parameters.AddWithValue("@OldFullName", oldstudents.FullName);
        } else {
            updateCommand.Parameters.AddWithValue("@OldFullName", DBNull.Value); }
        if (oldstudents.DateOfBirth.HasValue == true) {
            updateCommand.Parameters.AddWithValue("@OldDateOfBirth", oldstudents.DateOfBirth);
        } else {
            updateCommand.Parameters.AddWithValue("@OldDateOfBirth", DBNull.Value); }
        if (oldstudents.HomeAddress != null) {
            updateCommand.Parameters.AddWithValue("@OldHomeAddress", oldstudents.HomeAddress);
        } else {
            updateCommand.Parameters.AddWithValue("@OldHomeAddress", DBNull.Value); }
        updateCommand.Parameters.AddWithValue("@OldGender", oldstudents.Gender);
        if (oldstudents.Father != null) {
            updateCommand.Parameters.AddWithValue("@OldFather", oldstudents.Father);
        } else {
            updateCommand.Parameters.AddWithValue("@OldFather", DBNull.Value); }
        if (oldstudents.Mother != null) {
            updateCommand.Parameters.AddWithValue("@OldMother", oldstudents.Mother);
        } else {
            updateCommand.Parameters.AddWithValue("@OldMother", DBNull.Value); }
        if (oldstudents.ParentContact != null) {
            updateCommand.Parameters.AddWithValue("@OldParentContact", oldstudents.ParentContact);
        } else {
            updateCommand.Parameters.AddWithValue("@OldParentContact", DBNull.Value); }
        try
        {
            connection.Open();
            int count = updateCommand.ExecuteNonQuery();
            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (SqlException ex)
        {
            throw ex;
        }
        finally
        {
            connection.Close();
        }
    }

    public static bool Delete(students clsstudents)
    {
        SqlConnection connection = SchoolData.GetConnection();
        string deleteStatement
            = "DELETE FROM "  
            + "     [students] "
            + "WHERE " 
            + "     [StudentId] = @OldStudentId " 
            + " AND ((@OldClassId IS NULL AND [ClassId] IS NULL) OR [ClassId] = @OldClassId) " 
            + " AND ((@OldFullName IS NULL AND [FullName] IS NULL) OR [FullName] = @OldFullName) " 
            + " AND ((@OldDateOfBirth IS NULL AND [DateOfBirth] IS NULL) OR [DateOfBirth] = @OldDateOfBirth) " 
            + " AND ((@OldHomeAddress IS NULL AND [HomeAddress] IS NULL) OR [HomeAddress] = @OldHomeAddress) " 
            + " AND [Gender] = @OldGender " 
            + " AND ((@OldFather IS NULL AND [Father] IS NULL) OR [Father] = @OldFather) " 
            + " AND ((@OldMother IS NULL AND [Mother] IS NULL) OR [Mother] = @OldMother) " 
            + " AND ((@OldParentContact IS NULL AND [ParentContact] IS NULL) OR [ParentContact] = @OldParentContact) " 
            + "";
        SqlCommand deleteCommand = new SqlCommand(deleteStatement, connection);
        deleteCommand.CommandType = CommandType.Text;
        deleteCommand.Parameters.AddWithValue("@OldStudentId", clsstudents.StudentId);
        if (clsstudents.ClassId.HasValue == true) {
            deleteCommand.Parameters.AddWithValue("@OldClassId", clsstudents.ClassId);
        } else {
            deleteCommand.Parameters.AddWithValue("@OldClassId", DBNull.Value); }
        if (clsstudents.FullName != null) {
            deleteCommand.Parameters.AddWithValue("@OldFullName", clsstudents.FullName);
        } else {
            deleteCommand.Parameters.AddWithValue("@OldFullName", DBNull.Value); }
        if (clsstudents.DateOfBirth.HasValue == true) {
            deleteCommand.Parameters.AddWithValue("@OldDateOfBirth", clsstudents.DateOfBirth);
        } else {
            deleteCommand.Parameters.AddWithValue("@OldDateOfBirth", DBNull.Value); }
        if (clsstudents.HomeAddress != null) {
            deleteCommand.Parameters.AddWithValue("@OldHomeAddress", clsstudents.HomeAddress);
        } else {
            deleteCommand.Parameters.AddWithValue("@OldHomeAddress", DBNull.Value); }
        deleteCommand.Parameters.AddWithValue("@OldGender", clsstudents.Gender);
        if (clsstudents.Father != null) {
            deleteCommand.Parameters.AddWithValue("@OldFather", clsstudents.Father);
        } else {
            deleteCommand.Parameters.AddWithValue("@OldFather", DBNull.Value); }
        if (clsstudents.Mother != null) {
            deleteCommand.Parameters.AddWithValue("@OldMother", clsstudents.Mother);
        } else {
            deleteCommand.Parameters.AddWithValue("@OldMother", DBNull.Value); }
        if (clsstudents.ParentContact != null) {
            deleteCommand.Parameters.AddWithValue("@OldParentContact", clsstudents.ParentContact);
        } else {
            deleteCommand.Parameters.AddWithValue("@OldParentContact", DBNull.Value); }
        try
        {
            connection.Open();
            int count = deleteCommand.ExecuteNonQuery();
            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (SqlException ex)
        {
            throw ex;
        }
        finally
        {
            connection.Close();
        }
    }

}

 
