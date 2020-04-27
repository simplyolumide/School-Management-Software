using System;
using System.Data;
using System.Data.SqlClient;

public class examData
{

    public static DataTable SelectAll()
    {
        SqlConnection connection = SchoolData.GetConnection();
        string selectStatement
            = "SELECT "  
            + "     [Exam ID] = [exam].[ExamId] "
            + "    ,[Exam Type] = [exam].[ExamType] "
            + "FROM " 
            + "     [exam] " 
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
            + "     [exam].[ExamId] "
            + "    ,[exam].[ExamType] "
            + "FROM " 
            + "     [exam] " 
                + "WHERE " 
                + "     (@ExamId IS NULL OR @ExamId = '' OR [exam].[ExamId] LIKE '%' + LTRIM(RTRIM(@ExamId)) + '%') " 
                + "AND   (@ExamType IS NULL OR @ExamType = '' OR [exam].[ExamType] LIKE '%' + LTRIM(RTRIM(@ExamType)) + '%') " 
                + "";
        } else if (sCondition == "Equals") {
            selectStatement
                = "SELECT "
            + "     [exam].[ExamId] "
            + "    ,[exam].[ExamType] "
            + "FROM " 
            + "     [exam] " 
                + "WHERE " 
                + "     (@ExamId IS NULL OR @ExamId = '' OR [exam].[ExamId] = LTRIM(RTRIM(@ExamId))) " 
                + "AND   (@ExamType IS NULL OR @ExamType = '' OR [exam].[ExamType] = LTRIM(RTRIM(@ExamType))) " 
                + "";
        } else if  (sCondition == "Starts with...") {
            selectStatement
                = "SELECT "
            + "     [exam].[ExamId] "
            + "    ,[exam].[ExamType] "
            + "FROM " 
            + "     [exam] " 
                + "WHERE " 
                + "     (@ExamId IS NULL OR @ExamId = '' OR [exam].[ExamId] LIKE LTRIM(RTRIM(@ExamId)) + '%') " 
                + "AND   (@ExamType IS NULL OR @ExamType = '' OR [exam].[ExamType] LIKE LTRIM(RTRIM(@ExamType)) + '%') " 
                + "";
        } else if  (sCondition == "More than...") {
            selectStatement
                = "SELECT "
            + "     [exam].[ExamId] "
            + "    ,[exam].[ExamType] "
            + "FROM " 
            + "     [exam] " 
                + "WHERE " 
                + "     (@ExamId IS NULL OR @ExamId = '' OR [exam].[ExamId] > LTRIM(RTRIM(@ExamId))) " 
                + "AND   (@ExamType IS NULL OR @ExamType = '' OR [exam].[ExamType] > LTRIM(RTRIM(@ExamType))) " 
                + "";
        } else if  (sCondition == "Less than...") {
            selectStatement
                = "SELECT " 
            + "     [exam].[ExamId] "
            + "    ,[exam].[ExamType] "
            + "FROM " 
            + "     [exam] " 
                + "WHERE " 
                + "     (@ExamId IS NULL OR @ExamId = '' OR [exam].[ExamId] < LTRIM(RTRIM(@ExamId))) " 
                + "AND   (@ExamType IS NULL OR @ExamType = '' OR [exam].[ExamType] < LTRIM(RTRIM(@ExamType))) " 
                + "";
        } else if  (sCondition == "Equal or more than...") {
            selectStatement
                = "SELECT "
            + "     [exam].[ExamId] "
            + "    ,[exam].[ExamType] "
            + "FROM " 
            + "     [exam] " 
                + "WHERE " 
                + "     (@ExamId IS NULL OR @ExamId = '' OR [exam].[ExamId] >= LTRIM(RTRIM(@ExamId))) " 
                + "AND   (@ExamType IS NULL OR @ExamType = '' OR [exam].[ExamType] >= LTRIM(RTRIM(@ExamType))) " 
                + "";
        } else if (sCondition == "Equal or less than...") {
            selectStatement 
                = "SELECT "
            + "     [exam].[ExamId] "
            + "    ,[exam].[ExamType] "
            + "FROM " 
            + "     [exam] " 
                + "WHERE " 
                + "     (@ExamId IS NULL OR @ExamId = '' OR [exam].[ExamId] <= LTRIM(RTRIM(@ExamId))) " 
                + "AND   (@ExamType IS NULL OR @ExamType = '' OR [exam].[ExamType] <= LTRIM(RTRIM(@ExamType))) " 
                + "";
        }
        SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
        selectCommand.CommandType = CommandType.Text;
        if (sField == "Exam ID") {
            selectCommand.Parameters.AddWithValue("@ExamId", sValue);
        } else {
            selectCommand.Parameters.AddWithValue("@ExamId", DBNull.Value); }
        if (sField == "Exam Type") {
            selectCommand.Parameters.AddWithValue("@ExamType", sValue);
        } else {
            selectCommand.Parameters.AddWithValue("@ExamType", DBNull.Value); }
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

    public static exam Select_Record(exam clsexamPara)
    {
        exam clsexam = new exam();
        SqlConnection connection = SchoolData.GetConnection();
        string selectStatement
            = "SELECT " 
            + "     [ExamId] "
            + "    ,[ExamType] "
            + "FROM "
            + "     [exam] "
            + "WHERE "
            + "     [ExamId] = @ExamId "
            + "";
        SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
        selectCommand.CommandType = CommandType.Text;
        selectCommand.Parameters.AddWithValue("@ExamId", clsexamPara.ExamId);
        try
        {
            connection.Open();
            SqlDataReader reader
                = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
                clsexam.ExamId = System.Convert.ToInt32(reader["ExamId"]);
                clsexam.ExamType = reader["ExamType"] is DBNull ? null : reader["ExamType"].ToString();
            }
            else
            {
                clsexam = null;
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
        return clsexam;
    }

    public static bool Add(exam clsexam)
    {
        SqlConnection connection = SchoolData.GetConnection();
        string insertStatement
            = "INSERT " 
            + "     [exam] "
            + "     ( "
            + "     [ExamType] "
            + "     ) "
            + "VALUES " 
            + "     ( "
            + "     @ExamType "
            + "     ) "
            + "";
        SqlCommand insertCommand = new SqlCommand(insertStatement, connection);
        insertCommand.CommandType = CommandType.Text;
        if (clsexam.ExamType != null) {
            insertCommand.Parameters.AddWithValue("@ExamType", clsexam.ExamType);
        } else {
            insertCommand.Parameters.AddWithValue("@ExamType", DBNull.Value); }
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

    public static bool Update(exam oldexam, 
           exam newexam)
    {
        SqlConnection connection = SchoolData.GetConnection();
        string updateStatement
            = "UPDATE "  
            + "     [exam] "
            + "SET "
            + "     [ExamType] = @NewExamType "
            + "WHERE "
            + "     [ExamId] = @OldExamId " 
            + " AND ((@OldExamType IS NULL AND [ExamType] IS NULL) OR [ExamType] = @OldExamType) " 
            + "";
        SqlCommand updateCommand = new SqlCommand(updateStatement, connection);
        updateCommand.CommandType = CommandType.Text;
        if (newexam.ExamType != null) {
            updateCommand.Parameters.AddWithValue("@NewExamType", newexam.ExamType);
        } else {
            updateCommand.Parameters.AddWithValue("@NewExamType", DBNull.Value); }
        updateCommand.Parameters.AddWithValue("@OldExamId", oldexam.ExamId);
        if (oldexam.ExamType != null) {
            updateCommand.Parameters.AddWithValue("@OldExamType", oldexam.ExamType);
        } else {
            updateCommand.Parameters.AddWithValue("@OldExamType", DBNull.Value); }
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

    public static bool Delete(exam clsexam)
    {
        SqlConnection connection = SchoolData.GetConnection();
        string deleteStatement
            = "DELETE FROM "  
            + "     [exam] "
            + "WHERE " 
            + "     [ExamId] = @OldExamId " 
            + " AND ((@OldExamType IS NULL AND [ExamType] IS NULL) OR [ExamType] = @OldExamType) " 
            + "";
        SqlCommand deleteCommand = new SqlCommand(deleteStatement, connection);
        deleteCommand.CommandType = CommandType.Text;
        deleteCommand.Parameters.AddWithValue("@OldExamId", clsexam.ExamId);
        if (clsexam.ExamType != null) {
            deleteCommand.Parameters.AddWithValue("@OldExamType", clsexam.ExamType);
        } else {
            deleteCommand.Parameters.AddWithValue("@OldExamType", DBNull.Value); }
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

 
