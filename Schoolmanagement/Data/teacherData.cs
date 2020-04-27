using System;
using System.Data;
using System.Data.SqlClient;

public class teacherData
{

    public static DataTable SelectAll()
    {
        SqlConnection connection = SchoolData.GetConnection();
        string selectStatement
            = "SELECT "  
            + "     [Teacher ID] = [teacher].[TeacherId] "
            + "    ,[Class ID] = [studentclass].[ClassId] "
            + "    ,[Full Name] = [teacher].[FullName] "
            + "    ,[Date Of Join] = [teacher].[DateOfJoin] "
            + "    ,[Home Address] = [teacher].[HomeAddress] "
            + "    ,[Phone Number] = [teacher].[PhoneNumber] "
            + "FROM " 
            + "     [teacher] " 
            + "LEFT JOIN [studentclass] ON [teacher].[ClassId] = [studentclass].[ClassId] "
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
            + "     [teacher].[TeacherId] "
            + "    ,[studentclass].[ClassId] "
            + "    ,[teacher].[FullName] "
            + "    ,[teacher].[DateOfJoin] "
            + "    ,[teacher].[HomeAddress] "
            + "    ,[teacher].[PhoneNumber] "
            + "FROM " 
            + "     [teacher] " 
            + "LEFT JOIN [studentclass] ON [teacher].[ClassId] = [studentclass].[ClassId] "
                + "WHERE " 
                + "     (@TeacherId IS NULL OR @TeacherId = '' OR [teacher].[TeacherId] LIKE '%' + LTRIM(RTRIM(@TeacherId)) + '%') " 
                + "AND   (@ClassId IS NULL OR @ClassId = '' OR [studentclass].[ClassId] LIKE '%' + LTRIM(RTRIM(@ClassId)) + '%') " 
                + "AND   (@FullName IS NULL OR @FullName = '' OR [teacher].[FullName] LIKE '%' + LTRIM(RTRIM(@FullName)) + '%') " 
                + "AND   (@DateOfJoin IS NULL OR @DateOfJoin = '' OR [teacher].[DateOfJoin] LIKE '%' + LTRIM(RTRIM(@DateOfJoin)) + '%') " 
                + "AND   (@HomeAddress IS NULL OR @HomeAddress = '' OR [teacher].[HomeAddress] LIKE '%' + LTRIM(RTRIM(@HomeAddress)) + '%') " 
                + "AND   (@PhoneNumber IS NULL OR @PhoneNumber = '' OR [teacher].[PhoneNumber] LIKE '%' + LTRIM(RTRIM(@PhoneNumber)) + '%') " 
                + "";
        } else if (sCondition == "Equals") {
            selectStatement
                = "SELECT "
            + "     [teacher].[TeacherId] "
            + "    ,[studentclass].[ClassId] "
            + "    ,[teacher].[FullName] "
            + "    ,[teacher].[DateOfJoin] "
            + "    ,[teacher].[HomeAddress] "
            + "    ,[teacher].[PhoneNumber] "
            + "FROM " 
            + "     [teacher] " 
            + "LEFT JOIN [studentclass] ON [teacher].[ClassId] = [studentclass].[ClassId] "
                + "WHERE " 
                + "     (@TeacherId IS NULL OR @TeacherId = '' OR [teacher].[TeacherId] = LTRIM(RTRIM(@TeacherId))) " 
                + "AND   (@ClassId IS NULL OR @ClassId = '' OR [studentclass].[ClassId] = LTRIM(RTRIM(@ClassId))) " 
                + "AND   (@FullName IS NULL OR @FullName = '' OR [teacher].[FullName] = LTRIM(RTRIM(@FullName))) " 
                + "AND   (@DateOfJoin IS NULL OR @DateOfJoin = '' OR [teacher].[DateOfJoin] = LTRIM(RTRIM(@DateOfJoin))) " 
                + "AND   (@HomeAddress IS NULL OR @HomeAddress = '' OR [teacher].[HomeAddress] = LTRIM(RTRIM(@HomeAddress))) " 
                + "AND   (@PhoneNumber IS NULL OR @PhoneNumber = '' OR [teacher].[PhoneNumber] = LTRIM(RTRIM(@PhoneNumber))) " 
                + "";
        } else if  (sCondition == "Starts with...") {
            selectStatement
                = "SELECT "
            + "     [teacher].[TeacherId] "
            + "    ,[studentclass].[ClassId] "
            + "    ,[teacher].[FullName] "
            + "    ,[teacher].[DateOfJoin] "
            + "    ,[teacher].[HomeAddress] "
            + "    ,[teacher].[PhoneNumber] "
            + "FROM " 
            + "     [teacher] " 
            + "LEFT JOIN [studentclass] ON [teacher].[ClassId] = [studentclass].[ClassId] "
                + "WHERE " 
                + "     (@TeacherId IS NULL OR @TeacherId = '' OR [teacher].[TeacherId] LIKE LTRIM(RTRIM(@TeacherId)) + '%') " 
                + "AND   (@ClassId IS NULL OR @ClassId = '' OR [studentclass].[ClassId] LIKE LTRIM(RTRIM(@ClassId)) + '%') " 
                + "AND   (@FullName IS NULL OR @FullName = '' OR [teacher].[FullName] LIKE LTRIM(RTRIM(@FullName)) + '%') " 
                + "AND   (@DateOfJoin IS NULL OR @DateOfJoin = '' OR [teacher].[DateOfJoin] LIKE LTRIM(RTRIM(@DateOfJoin)) + '%') " 
                + "AND   (@HomeAddress IS NULL OR @HomeAddress = '' OR [teacher].[HomeAddress] LIKE LTRIM(RTRIM(@HomeAddress)) + '%') " 
                + "AND   (@PhoneNumber IS NULL OR @PhoneNumber = '' OR [teacher].[PhoneNumber] LIKE LTRIM(RTRIM(@PhoneNumber)) + '%') " 
                + "";
        } else if  (sCondition == "More than...") {
            selectStatement
                = "SELECT "
            + "     [teacher].[TeacherId] "
            + "    ,[studentclass].[ClassId] "
            + "    ,[teacher].[FullName] "
            + "    ,[teacher].[DateOfJoin] "
            + "    ,[teacher].[HomeAddress] "
            + "    ,[teacher].[PhoneNumber] "
            + "FROM " 
            + "     [teacher] " 
            + "LEFT JOIN [studentclass] ON [teacher].[ClassId] = [studentclass].[ClassId] "
                + "WHERE " 
                + "     (@TeacherId IS NULL OR @TeacherId = '' OR [teacher].[TeacherId] > LTRIM(RTRIM(@TeacherId))) " 
                + "AND   (@ClassId IS NULL OR @ClassId = '' OR [studentclass].[ClassId] > LTRIM(RTRIM(@ClassId))) " 
                + "AND   (@FullName IS NULL OR @FullName = '' OR [teacher].[FullName] > LTRIM(RTRIM(@FullName))) " 
                + "AND   (@DateOfJoin IS NULL OR @DateOfJoin = '' OR [teacher].[DateOfJoin] > LTRIM(RTRIM(@DateOfJoin))) " 
                + "AND   (@HomeAddress IS NULL OR @HomeAddress = '' OR [teacher].[HomeAddress] > LTRIM(RTRIM(@HomeAddress))) " 
                + "AND   (@PhoneNumber IS NULL OR @PhoneNumber = '' OR [teacher].[PhoneNumber] > LTRIM(RTRIM(@PhoneNumber))) " 
                + "";
        } else if  (sCondition == "Less than...") {
            selectStatement
                = "SELECT " 
            + "     [teacher].[TeacherId] "
            + "    ,[studentclass].[ClassId] "
            + "    ,[teacher].[FullName] "
            + "    ,[teacher].[DateOfJoin] "
            + "    ,[teacher].[HomeAddress] "
            + "    ,[teacher].[PhoneNumber] "
            + "FROM " 
            + "     [teacher] " 
            + "LEFT JOIN [studentclass] ON [teacher].[ClassId] = [studentclass].[ClassId] "
                + "WHERE " 
                + "     (@TeacherId IS NULL OR @TeacherId = '' OR [teacher].[TeacherId] < LTRIM(RTRIM(@TeacherId))) " 
                + "AND   (@ClassId IS NULL OR @ClassId = '' OR [studentclass].[ClassId] < LTRIM(RTRIM(@ClassId))) " 
                + "AND   (@FullName IS NULL OR @FullName = '' OR [teacher].[FullName] < LTRIM(RTRIM(@FullName))) " 
                + "AND   (@DateOfJoin IS NULL OR @DateOfJoin = '' OR [teacher].[DateOfJoin] < LTRIM(RTRIM(@DateOfJoin))) " 
                + "AND   (@HomeAddress IS NULL OR @HomeAddress = '' OR [teacher].[HomeAddress] < LTRIM(RTRIM(@HomeAddress))) " 
                + "AND   (@PhoneNumber IS NULL OR @PhoneNumber = '' OR [teacher].[PhoneNumber] < LTRIM(RTRIM(@PhoneNumber))) " 
                + "";
        } else if  (sCondition == "Equal or more than...") {
            selectStatement
                = "SELECT "
            + "     [teacher].[TeacherId] "
            + "    ,[studentclass].[ClassId] "
            + "    ,[teacher].[FullName] "
            + "    ,[teacher].[DateOfJoin] "
            + "    ,[teacher].[HomeAddress] "
            + "    ,[teacher].[PhoneNumber] "
            + "FROM " 
            + "     [teacher] " 
            + "LEFT JOIN [studentclass] ON [teacher].[ClassId] = [studentclass].[ClassId] "
                + "WHERE " 
                + "     (@TeacherId IS NULL OR @TeacherId = '' OR [teacher].[TeacherId] >= LTRIM(RTRIM(@TeacherId))) " 
                + "AND   (@ClassId IS NULL OR @ClassId = '' OR [studentclass].[ClassId] >= LTRIM(RTRIM(@ClassId))) " 
                + "AND   (@FullName IS NULL OR @FullName = '' OR [teacher].[FullName] >= LTRIM(RTRIM(@FullName))) " 
                + "AND   (@DateOfJoin IS NULL OR @DateOfJoin = '' OR [teacher].[DateOfJoin] >= LTRIM(RTRIM(@DateOfJoin))) " 
                + "AND   (@HomeAddress IS NULL OR @HomeAddress = '' OR [teacher].[HomeAddress] >= LTRIM(RTRIM(@HomeAddress))) " 
                + "AND   (@PhoneNumber IS NULL OR @PhoneNumber = '' OR [teacher].[PhoneNumber] >= LTRIM(RTRIM(@PhoneNumber))) " 
                + "";
        } else if (sCondition == "Equal or less than...") {
            selectStatement 
                = "SELECT "
            + "     [teacher].[TeacherId] "
            + "    ,[studentclass].[ClassId] "
            + "    ,[teacher].[FullName] "
            + "    ,[teacher].[DateOfJoin] "
            + "    ,[teacher].[HomeAddress] "
            + "    ,[teacher].[PhoneNumber] "
            + "FROM " 
            + "     [teacher] " 
            + "LEFT JOIN [studentclass] ON [teacher].[ClassId] = [studentclass].[ClassId] "
                + "WHERE " 
                + "     (@TeacherId IS NULL OR @TeacherId = '' OR [teacher].[TeacherId] <= LTRIM(RTRIM(@TeacherId))) " 
                + "AND   (@ClassId IS NULL OR @ClassId = '' OR [studentclass].[ClassId] <= LTRIM(RTRIM(@ClassId))) " 
                + "AND   (@FullName IS NULL OR @FullName = '' OR [teacher].[FullName] <= LTRIM(RTRIM(@FullName))) " 
                + "AND   (@DateOfJoin IS NULL OR @DateOfJoin = '' OR [teacher].[DateOfJoin] <= LTRIM(RTRIM(@DateOfJoin))) " 
                + "AND   (@HomeAddress IS NULL OR @HomeAddress = '' OR [teacher].[HomeAddress] <= LTRIM(RTRIM(@HomeAddress))) " 
                + "AND   (@PhoneNumber IS NULL OR @PhoneNumber = '' OR [teacher].[PhoneNumber] <= LTRIM(RTRIM(@PhoneNumber))) " 
                + "";
        }
        SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
        selectCommand.CommandType = CommandType.Text;
        if (sField == "Teacher ID") {
            selectCommand.Parameters.AddWithValue("@TeacherId", sValue);
        } else {
            selectCommand.Parameters.AddWithValue("@TeacherId", DBNull.Value); }
        if (sField == "Class ID") {
            selectCommand.Parameters.AddWithValue("@ClassId", sValue);
        } else {
            selectCommand.Parameters.AddWithValue("@ClassId", DBNull.Value); }
        if (sField == "Full Name") {
            selectCommand.Parameters.AddWithValue("@FullName", sValue);
        } else {
            selectCommand.Parameters.AddWithValue("@FullName", DBNull.Value); }
        if (sField == "Date Of Join") {
            selectCommand.Parameters.AddWithValue("@DateOfJoin", sValue);
        } else {
            selectCommand.Parameters.AddWithValue("@DateOfJoin", DBNull.Value); }
        if (sField == "Home Address") {
            selectCommand.Parameters.AddWithValue("@HomeAddress", sValue);
        } else {
            selectCommand.Parameters.AddWithValue("@HomeAddress", DBNull.Value); }
        if (sField == "Phone Number") {
            selectCommand.Parameters.AddWithValue("@PhoneNumber", sValue);
        } else {
            selectCommand.Parameters.AddWithValue("@PhoneNumber", DBNull.Value); }
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

    public static teacher Select_Record(teacher clsteacherPara)
    {
        teacher clsteacher = new teacher();
        SqlConnection connection = SchoolData.GetConnection();
        string selectStatement
            = "SELECT " 
            + "     [TeacherId] "
            + "    ,[ClassId] "
            + "    ,[FullName] "
            + "    ,[DateOfJoin] "
            + "    ,[HomeAddress] "
            + "    ,[PhoneNumber] "
            + "FROM "
            + "     [teacher] "
            + "WHERE "
            + "     [TeacherId] = @TeacherId "
            + "";
        SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
        selectCommand.CommandType = CommandType.Text;
        selectCommand.Parameters.AddWithValue("@TeacherId", clsteacherPara.TeacherId);
        try
        {
            connection.Open();
            SqlDataReader reader
                = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
                clsteacher.TeacherId = System.Convert.ToInt32(reader["TeacherId"]);
                clsteacher.ClassId = reader["ClassId"] is DBNull ? null : (Int32?)reader["ClassId"];
                clsteacher.FullName = reader["FullName"] is DBNull ? null : reader["FullName"].ToString();
                clsteacher.DateOfJoin = reader["DateOfJoin"] is DBNull ? null : (DateTime?)reader["DateOfJoin"];
                clsteacher.HomeAddress = reader["HomeAddress"] is DBNull ? null : reader["HomeAddress"].ToString();
                clsteacher.PhoneNumber = reader["PhoneNumber"] is DBNull ? null : reader["PhoneNumber"].ToString();
            }
            else
            {
                clsteacher = null;
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
        return clsteacher;
    }

    public static bool Add(teacher clsteacher)
    {
        SqlConnection connection = SchoolData.GetConnection();
        string insertStatement
            = "INSERT " 
            + "     [teacher] "
            + "     ( "
            + "     [ClassId] "
            + "    ,[FullName] "
            + "    ,[DateOfJoin] "
            + "    ,[HomeAddress] "
            + "    ,[PhoneNumber] "
            + "     ) "
            + "VALUES " 
            + "     ( "
            + "     @ClassId "
            + "    ,@FullName "
            + "    ,@DateOfJoin "
            + "    ,@HomeAddress "
            + "    ,@PhoneNumber "
            + "     ) "
            + "";
        SqlCommand insertCommand = new SqlCommand(insertStatement, connection);
        insertCommand.CommandType = CommandType.Text;
        if (clsteacher.ClassId.HasValue == true) {
            insertCommand.Parameters.AddWithValue("@ClassId", clsteacher.ClassId);
        } else {
            insertCommand.Parameters.AddWithValue("@ClassId", DBNull.Value); }
        if (clsteacher.FullName != null) {
            insertCommand.Parameters.AddWithValue("@FullName", clsteacher.FullName);
        } else {
            insertCommand.Parameters.AddWithValue("@FullName", DBNull.Value); }
        if (clsteacher.DateOfJoin.HasValue == true) {
            insertCommand.Parameters.AddWithValue("@DateOfJoin", clsteacher.DateOfJoin);
        } else {
            insertCommand.Parameters.AddWithValue("@DateOfJoin", DBNull.Value); }
        if (clsteacher.HomeAddress != null) {
            insertCommand.Parameters.AddWithValue("@HomeAddress", clsteacher.HomeAddress);
        } else {
            insertCommand.Parameters.AddWithValue("@HomeAddress", DBNull.Value); }
        if (clsteacher.PhoneNumber != null) {
            insertCommand.Parameters.AddWithValue("@PhoneNumber", clsteacher.PhoneNumber);
        } else {
            insertCommand.Parameters.AddWithValue("@PhoneNumber", DBNull.Value); }
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

    public static bool Update(teacher oldteacher, 
           teacher newteacher)
    {
        SqlConnection connection = SchoolData.GetConnection();
        string updateStatement
            = "UPDATE "  
            + "     [teacher] "
            + "SET "
            + "     [ClassId] = @NewClassId "
            + "    ,[FullName] = @NewFullName "
            + "    ,[DateOfJoin] = @NewDateOfJoin "
            + "    ,[HomeAddress] = @NewHomeAddress "
            + "    ,[PhoneNumber] = @NewPhoneNumber "
            + "WHERE "
            + "     [TeacherId] = @OldTeacherId " 
            + " AND ((@OldClassId IS NULL AND [ClassId] IS NULL) OR [ClassId] = @OldClassId) " 
            + " AND ((@OldFullName IS NULL AND [FullName] IS NULL) OR [FullName] = @OldFullName) " 
            + " AND ((@OldDateOfJoin IS NULL AND [DateOfJoin] IS NULL) OR [DateOfJoin] = @OldDateOfJoin) " 
            + " AND ((@OldHomeAddress IS NULL AND [HomeAddress] IS NULL) OR [HomeAddress] = @OldHomeAddress) " 
            + " AND ((@OldPhoneNumber IS NULL AND [PhoneNumber] IS NULL) OR [PhoneNumber] = @OldPhoneNumber) " 
            + "";
        SqlCommand updateCommand = new SqlCommand(updateStatement, connection);
        updateCommand.CommandType = CommandType.Text;
        if (newteacher.ClassId.HasValue == true) {
            updateCommand.Parameters.AddWithValue("@NewClassId", newteacher.ClassId);
        } else {
            updateCommand.Parameters.AddWithValue("@NewClassId", DBNull.Value); }
        if (newteacher.FullName != null) {
            updateCommand.Parameters.AddWithValue("@NewFullName", newteacher.FullName);
        } else {
            updateCommand.Parameters.AddWithValue("@NewFullName", DBNull.Value); }
        if (newteacher.DateOfJoin.HasValue == true) {
            updateCommand.Parameters.AddWithValue("@NewDateOfJoin", newteacher.DateOfJoin);
        } else {
            updateCommand.Parameters.AddWithValue("@NewDateOfJoin", DBNull.Value); }
        if (newteacher.HomeAddress != null) {
            updateCommand.Parameters.AddWithValue("@NewHomeAddress", newteacher.HomeAddress);
        } else {
            updateCommand.Parameters.AddWithValue("@NewHomeAddress", DBNull.Value); }
        if (newteacher.PhoneNumber != null) {
            updateCommand.Parameters.AddWithValue("@NewPhoneNumber", newteacher.PhoneNumber);
        } else {
            updateCommand.Parameters.AddWithValue("@NewPhoneNumber", DBNull.Value); }
        updateCommand.Parameters.AddWithValue("@OldTeacherId", oldteacher.TeacherId);
        if (oldteacher.ClassId.HasValue == true) {
            updateCommand.Parameters.AddWithValue("@OldClassId", oldteacher.ClassId);
        } else {
            updateCommand.Parameters.AddWithValue("@OldClassId", DBNull.Value); }
        if (oldteacher.FullName != null) {
            updateCommand.Parameters.AddWithValue("@OldFullName", oldteacher.FullName);
        } else {
            updateCommand.Parameters.AddWithValue("@OldFullName", DBNull.Value); }
        if (oldteacher.DateOfJoin.HasValue == true) {
            updateCommand.Parameters.AddWithValue("@OldDateOfJoin", oldteacher.DateOfJoin);
        } else {
            updateCommand.Parameters.AddWithValue("@OldDateOfJoin", DBNull.Value); }
        if (oldteacher.HomeAddress != null) {
            updateCommand.Parameters.AddWithValue("@OldHomeAddress", oldteacher.HomeAddress);
        } else {
            updateCommand.Parameters.AddWithValue("@OldHomeAddress", DBNull.Value); }
        if (oldteacher.PhoneNumber != null) {
            updateCommand.Parameters.AddWithValue("@OldPhoneNumber", oldteacher.PhoneNumber);
        } else {
            updateCommand.Parameters.AddWithValue("@OldPhoneNumber", DBNull.Value); }
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

    public static bool Delete(teacher clsteacher)
    {
        SqlConnection connection = SchoolData.GetConnection();
        string deleteStatement
            = "DELETE FROM "  
            + "     [teacher] "
            + "WHERE " 
            + "     [TeacherId] = @OldTeacherId " 
            + " AND ((@OldClassId IS NULL AND [ClassId] IS NULL) OR [ClassId] = @OldClassId) " 
            + " AND ((@OldFullName IS NULL AND [FullName] IS NULL) OR [FullName] = @OldFullName) " 
            + " AND ((@OldDateOfJoin IS NULL AND [DateOfJoin] IS NULL) OR [DateOfJoin] = @OldDateOfJoin) " 
            + " AND ((@OldHomeAddress IS NULL AND [HomeAddress] IS NULL) OR [HomeAddress] = @OldHomeAddress) " 
            + " AND ((@OldPhoneNumber IS NULL AND [PhoneNumber] IS NULL) OR [PhoneNumber] = @OldPhoneNumber) " 
            + "";
        SqlCommand deleteCommand = new SqlCommand(deleteStatement, connection);
        deleteCommand.CommandType = CommandType.Text;
        deleteCommand.Parameters.AddWithValue("@OldTeacherId", clsteacher.TeacherId);
        if (clsteacher.ClassId.HasValue == true) {
            deleteCommand.Parameters.AddWithValue("@OldClassId", clsteacher.ClassId);
        } else {
            deleteCommand.Parameters.AddWithValue("@OldClassId", DBNull.Value); }
        if (clsteacher.FullName != null) {
            deleteCommand.Parameters.AddWithValue("@OldFullName", clsteacher.FullName);
        } else {
            deleteCommand.Parameters.AddWithValue("@OldFullName", DBNull.Value); }
        if (clsteacher.DateOfJoin.HasValue == true) {
            deleteCommand.Parameters.AddWithValue("@OldDateOfJoin", clsteacher.DateOfJoin);
        } else {
            deleteCommand.Parameters.AddWithValue("@OldDateOfJoin", DBNull.Value); }
        if (clsteacher.HomeAddress != null) {
            deleteCommand.Parameters.AddWithValue("@OldHomeAddress", clsteacher.HomeAddress);
        } else {
            deleteCommand.Parameters.AddWithValue("@OldHomeAddress", DBNull.Value); }
        if (clsteacher.PhoneNumber != null) {
            deleteCommand.Parameters.AddWithValue("@OldPhoneNumber", clsteacher.PhoneNumber);
        } else {
            deleteCommand.Parameters.AddWithValue("@OldPhoneNumber", DBNull.Value); }
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

 
