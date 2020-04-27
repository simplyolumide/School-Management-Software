using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

public class students_studentclassData
{
    public static List<students_studentclass> List()
    {
        List<students_studentclass> students_studentclassList = new List<students_studentclass>();
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
            students_studentclass clsstudents_studentclass = new students_studentclass();
            while (reader.Read())
            {
                clsstudents_studentclass = new students_studentclass();
                clsstudents_studentclass.ClassId = System.Convert.ToInt32(reader["ClassId"]);
                students_studentclassList.Add(clsstudents_studentclass);
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
        return students_studentclassList;
    }

}


 
