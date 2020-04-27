using System;
using System.Data;
using System.Data.SqlClient;

public class studentclassData
{

    public static DataTable SelectAll()
    {
        SqlConnection connection = SchoolData.GetConnection();
        string selectStatement
            = "SELECT "  
            + "     [Class ID] = [studentclass].[ClassId] "
            + "    ,[Class Name] = [studentclass].[ClassName] "
            + "FROM " 
            + "     [studentclass] " 
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
            + "     [studentclass].[ClassId] "
            + "    ,[studentclass].[ClassName] "
            + "FROM " 
            + "     [studentclass] " 
                + "WHERE " 
                + "     (@ClassId IS NULL OR @ClassId = '' OR [studentclass].[ClassId] LIKE '%' + LTRIM(RTRIM(@ClassId)) + '%') " 
                + "AND   (@ClassName IS NULL OR @ClassName = '' OR [studentclass].[ClassName] LIKE '%' + LTRIM(RTRIM(@ClassName)) + '%') " 
                + "";
        } else if (sCondition == "Equals") {
            selectStatement
                = "SELECT "
            + "     [studentclass].[ClassId] "
            + "    ,[studentclass].[ClassName] "
            + "FROM " 
            + "     [studentclass] " 
                + "WHERE " 
                + "     (@ClassId IS NULL OR @ClassId = '' OR [studentclass].[ClassId] = LTRIM(RTRIM(@ClassId))) " 
                + "AND   (@ClassName IS NULL OR @ClassName = '' OR [studentclass].[ClassName] = LTRIM(RTRIM(@ClassName))) " 
                + "";
        } else if  (sCondition == "Starts with...") {
            selectStatement
                = "SELECT "
            + "     [studentclass].[ClassId] "
            + "    ,[studentclass].[ClassName] "
            + "FROM " 
            + "     [studentclass] " 
                + "WHERE " 
                + "     (@ClassId IS NULL OR @ClassId = '' OR [studentclass].[ClassId] LIKE LTRIM(RTRIM(@ClassId)) + '%') " 
                + "AND   (@ClassName IS NULL OR @ClassName = '' OR [studentclass].[ClassName] LIKE LTRIM(RTRIM(@ClassName)) + '%') " 
                + "";
        } else if  (sCondition == "More than...") {
            selectStatement
                = "SELECT "
            + "     [studentclass].[ClassId] "
            + "    ,[studentclass].[ClassName] "
            + "FROM " 
            + "     [studentclass] " 
                + "WHERE " 
                + "     (@ClassId IS NULL OR @ClassId = '' OR [studentclass].[ClassId] > LTRIM(RTRIM(@ClassId))) " 
                + "AND   (@ClassName IS NULL OR @ClassName = '' OR [studentclass].[ClassName] > LTRIM(RTRIM(@ClassName))) " 
                + "";
        } else if  (sCondition == "Less than...") {
            selectStatement
                = "SELECT " 
            + "     [studentclass].[ClassId] "
            + "    ,[studentclass].[ClassName] "
            + "FROM " 
            + "     [studentclass] " 
                + "WHERE " 
                + "     (@ClassId IS NULL OR @ClassId = '' OR [studentclass].[ClassId] < LTRIM(RTRIM(@ClassId))) " 
                + "AND   (@ClassName IS NULL OR @ClassName = '' OR [studentclass].[ClassName] < LTRIM(RTRIM(@ClassName))) " 
                + "";
        } else if  (sCondition == "Equal or more than...") {
            selectStatement
                = "SELECT "
            + "     [studentclass].[ClassId] "
            + "    ,[studentclass].[ClassName] "
            + "FROM " 
            + "     [studentclass] " 
                + "WHERE " 
                + "     (@ClassId IS NULL OR @ClassId = '' OR [studentclass].[ClassId] >= LTRIM(RTRIM(@ClassId))) " 
                + "AND   (@ClassName IS NULL OR @ClassName = '' OR [studentclass].[ClassName] >= LTRIM(RTRIM(@ClassName))) " 
                + "";
        } else if (sCondition == "Equal or less than...") {
            selectStatement 
                = "SELECT "
            + "     [studentclass].[ClassId] "
            + "    ,[studentclass].[ClassName] "
            + "FROM " 
            + "     [studentclass] " 
                + "WHERE " 
                + "     (@ClassId IS NULL OR @ClassId = '' OR [studentclass].[ClassId] <= LTRIM(RTRIM(@ClassId))) " 
                + "AND   (@ClassName IS NULL OR @ClassName = '' OR [studentclass].[ClassName] <= LTRIM(RTRIM(@ClassName))) " 
                + "";
        }
        SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
        selectCommand.CommandType = CommandType.Text;
        if (sField == "Class ID") {
            selectCommand.Parameters.AddWithValue("@ClassId", sValue);
        } else {
            selectCommand.Parameters.AddWithValue("@ClassId", DBNull.Value); }
        if (sField == "Class Name") {
            selectCommand.Parameters.AddWithValue("@ClassName", sValue);
        } else {
            selectCommand.Parameters.AddWithValue("@ClassName", DBNull.Value); }
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

    public static studentclass Select_Record(studentclass clsstudentclassPara)
    {
        studentclass clsstudentclass = new studentclass();
        SqlConnection connection = SchoolData.GetConnection();
        string selectStatement
            = "SELECT " 
            + "     [ClassId] "
            + "    ,[ClassName] "
            + "FROM "
            + "     [studentclass] "
            + "WHERE "
            + "     [ClassId] = @ClassId "
            + "";
        SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
        selectCommand.CommandType = CommandType.Text;
        selectCommand.Parameters.AddWithValue("@ClassId", clsstudentclassPara.ClassId);
        try
        {
            connection.Open();
            SqlDataReader reader
                = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
                clsstudentclass.ClassId = System.Convert.ToInt32(reader["ClassId"]);
                clsstudentclass.ClassName = reader["ClassName"] is DBNull ? null : reader["ClassName"].ToString();
            }
            else
            {
                clsstudentclass = null;
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
        return clsstudentclass;
    }

    public static bool Add(studentclass clsstudentclass)
    {
        SqlConnection connection = SchoolData.GetConnection();
        string insertStatement
            = "INSERT " 
            + "     [studentclass] "
            + "     ( "
            + "     [ClassName] "
            + "     ) "
            + "VALUES " 
            + "     ( "
            + "     @ClassName "
            + "     ) "
            + "";
        SqlCommand insertCommand = new SqlCommand(insertStatement, connection);
        insertCommand.CommandType = CommandType.Text;
        if (clsstudentclass.ClassName != null) {
            insertCommand.Parameters.AddWithValue("@ClassName", clsstudentclass.ClassName);
        } else {
            insertCommand.Parameters.AddWithValue("@ClassName", DBNull.Value); }
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

    public static bool Update(studentclass oldstudentclass, 
           studentclass newstudentclass)
    {
        SqlConnection connection = SchoolData.GetConnection();
        string updateStatement
            = "UPDATE "  
            + "     [studentclass] "
            + "SET "
            + "     [ClassName] = @NewClassName "
            + "WHERE "
            + "     [ClassId] = @OldClassId " 
            + " AND ((@OldClassName IS NULL AND [ClassName] IS NULL) OR [ClassName] = @OldClassName) " 
            + "";
        SqlCommand updateCommand = new SqlCommand(updateStatement, connection);
        updateCommand.CommandType = CommandType.Text;
        if (newstudentclass.ClassName != null) {
            updateCommand.Parameters.AddWithValue("@NewClassName", newstudentclass.ClassName);
        } else {
            updateCommand.Parameters.AddWithValue("@NewClassName", DBNull.Value); }
        updateCommand.Parameters.AddWithValue("@OldClassId", oldstudentclass.ClassId);
        if (oldstudentclass.ClassName != null) {
            updateCommand.Parameters.AddWithValue("@OldClassName", oldstudentclass.ClassName);
        } else {
            updateCommand.Parameters.AddWithValue("@OldClassName", DBNull.Value); }
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

    public static bool Delete(studentclass clsstudentclass)
    {
        SqlConnection connection = SchoolData.GetConnection();
        string deleteStatement
            = "DELETE FROM "  
            + "     [studentclass] "
            + "WHERE " 
            + "     [ClassId] = @OldClassId " 
            + " AND ((@OldClassName IS NULL AND [ClassName] IS NULL) OR [ClassName] = @OldClassName) " 
            + "";
        SqlCommand deleteCommand = new SqlCommand(deleteStatement, connection);
        deleteCommand.CommandType = CommandType.Text;
        deleteCommand.Parameters.AddWithValue("@OldClassId", clsstudentclass.ClassId);
        if (clsstudentclass.ClassName != null) {
            deleteCommand.Parameters.AddWithValue("@OldClassName", clsstudentclass.ClassName);
        } else {
            deleteCommand.Parameters.AddWithValue("@OldClassName", DBNull.Value); }
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

 
