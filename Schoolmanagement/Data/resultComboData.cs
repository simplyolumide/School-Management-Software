using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

public class result_examData4
{
    public static List<result_exam4> List()
    {
        List<result_exam4> result_examList = new List<result_exam4>();
        SqlConnection connection = SchoolData.GetConnection();
        string selectStatement 
            = "SELECT "  
            + "     [ExamId] " 
            + "FROM " 
            + "     [exam] " 
            + "";
        SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
        try
        {
            connection.Open();
            SqlDataReader reader = selectCommand.ExecuteReader();
            result_exam4 clsresult_exam = new result_exam4();
            while (reader.Read())
            {
                clsresult_exam = new result_exam4();
                clsresult_exam.ExamId = System.Convert.ToInt32(reader["ExamId"]);
                result_examList.Add(clsresult_exam);
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
        return result_examList;
    }

}

public class result_studentsData5
{
    public static List<result_students5> List()
    {
        List<result_students5> result_studentsList = new List<result_students5>();
        SqlConnection connection = SchoolData.GetConnection();
        string selectStatement 
            = "SELECT "  
            + "     [StudentId] " 
            + "FROM " 
            + "     [students] " 
            + "";
        SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
        try
        {
            connection.Open();
            SqlDataReader reader = selectCommand.ExecuteReader();
            result_students5 clsresult_students = new result_students5();
            while (reader.Read())
            {
                clsresult_students = new result_students5();
                clsresult_students.StudentId = System.Convert.ToInt32(reader["StudentId"]);
                result_studentsList.Add(clsresult_students);
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
        return result_studentsList;
    }

}


 
