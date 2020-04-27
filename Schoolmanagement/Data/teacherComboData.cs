using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

public class teacher_studentclassData
{
    public static List<teacher_studentclass> List()
    {
        List<teacher_studentclass> teacher_studentclassList = new List<teacher_studentclass>();
        SqlConnection connection = SchoolData.GetConnection();
        string selectStatement 
            = "SELECT "  
            + "     [ClassId] " 
            + "FROM " 
            + "     [studentclass] " 
            + "";
        SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
        try
        {
            connection.Open();
            SqlDataReader reader = selectCommand.ExecuteReader();
            teacher_studentclass clsteacher_studentclass = new teacher_studentclass();
            while (reader.Read())
            {
                clsteacher_studentclass = new teacher_studentclass();
                clsteacher_studentclass.ClassId = System.Convert.ToInt32(reader["ClassId"]);
                teacher_studentclassList.Add(clsteacher_studentclass);
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
        return teacher_studentclassList;
    }

}


 
