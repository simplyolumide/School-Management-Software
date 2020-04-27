using System;
using System.Data;
using System.Data.SqlClient;

public class resultData
{

    public static DataTable SelectAll()
    {
        SqlConnection connection = SchoolData.GetConnection();
        string selectStatement
            = "SELECT "  
            + "     [Result ID] = [result].[ResultId] "
            + "    ,[Exam ID] = [A4].[ExamId] "
            + "    ,[Student ID] = [A5].[StudentId] "
            + "    ,[Mathematics] = [result].[Mathematics] "
            + "    ,[English Language] = [result].[EnglishLanguage] "
            + "    ,[Quantitative Reasoning] = [result].[QuantitativeReasoning] "
            + "    ,[Elementary Science] = [result].[ElementaryScience] "
            + "    ,[Social Studies] = [result].[SocialStudies] "
            + "    ,[Vocational Aptitude] = [result].[VocationalAptitude] "
            + "    ,[Health Education] = [result].[HealthEducation] "
            + "    ,[Computer] = [result].[Computer] "
            + "    ,[Home Economics] = [result].[HomeEconomics] "
            + "    ,[Moral Instruction] = [result].[MoralInstruction] "
            + "    ,[Total Score] = [result].[TotalScore] "
            + "    ,[Grade] = [result].[Grade] "
            + "FROM " 
            + "     [result] " 
            + "LEFT JOIN [exam] as [A4] ON [result].[ExamId] = [A4].[ExamId] "
            + "LEFT JOIN [students] as [A5] ON [result].[StudentId] = [A5].[StudentId] "
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
            + "     [result].[ResultId] "
            + "    ,[A4].[ExamId] as [ExamId4]"
            + "    ,[A5].[StudentId] as [StudentId5]"
            + "    ,[result].[Mathematics] "
            + "    ,[result].[EnglishLanguage] "
            + "    ,[result].[QuantitativeReasoning] "
            + "    ,[result].[ElementaryScience] "
            + "    ,[result].[SocialStudies] "
            + "    ,[result].[VocationalAptitude] "
            + "    ,[result].[HealthEducation] "
            + "    ,[result].[Computer] "
            + "    ,[result].[HomeEconomics] "
            + "    ,[result].[MoralInstruction] "
            + "    ,[result].[TotalScore] "
            + "    ,[result].[Grade] "
            + "FROM " 
            + "     [result] " 
            + "LEFT JOIN [exam] as [A4] ON [result].[ExamId] = [A4].[ExamId] "
            + "LEFT JOIN [students] as [A5] ON [result].[StudentId] = [A5].[StudentId] "
                + "WHERE " 
                + "     (@ResultId IS NULL OR @ResultId = '' OR [result].[ResultId] LIKE '%' + LTRIM(RTRIM(@ResultId)) + '%') " 
                + "AND   (@ExamId4 IS NULL OR @ExamId4 = '' OR [A4].[ExamId] LIKE '%' + LTRIM(RTRIM(@ExamId4)) + '%') " 
                + "AND   (@StudentId5 IS NULL OR @StudentId5 = '' OR [A5].[StudentId] LIKE '%' + LTRIM(RTRIM(@StudentId5)) + '%') " 
                + "AND   (@Mathematics IS NULL OR @Mathematics = '' OR [result].[Mathematics] LIKE '%' + LTRIM(RTRIM(@Mathematics)) + '%') " 
                + "AND   (@EnglishLanguage IS NULL OR @EnglishLanguage = '' OR [result].[EnglishLanguage] LIKE '%' + LTRIM(RTRIM(@EnglishLanguage)) + '%') " 
                + "AND   (@QuantitativeReasoning IS NULL OR @QuantitativeReasoning = '' OR [result].[QuantitativeReasoning] LIKE '%' + LTRIM(RTRIM(@QuantitativeReasoning)) + '%') " 
                + "AND   (@ElementaryScience IS NULL OR @ElementaryScience = '' OR [result].[ElementaryScience] LIKE '%' + LTRIM(RTRIM(@ElementaryScience)) + '%') " 
                + "AND   (@SocialStudies IS NULL OR @SocialStudies = '' OR [result].[SocialStudies] LIKE '%' + LTRIM(RTRIM(@SocialStudies)) + '%') " 
                + "AND   (@VocationalAptitude IS NULL OR @VocationalAptitude = '' OR [result].[VocationalAptitude] LIKE '%' + LTRIM(RTRIM(@VocationalAptitude)) + '%') " 
                + "AND   (@HealthEducation IS NULL OR @HealthEducation = '' OR [result].[HealthEducation] LIKE '%' + LTRIM(RTRIM(@HealthEducation)) + '%') " 
                + "AND   (@Computer IS NULL OR @Computer = '' OR [result].[Computer] LIKE '%' + LTRIM(RTRIM(@Computer)) + '%') " 
                + "AND   (@HomeEconomics IS NULL OR @HomeEconomics = '' OR [result].[HomeEconomics] LIKE '%' + LTRIM(RTRIM(@HomeEconomics)) + '%') " 
                + "AND   (@MoralInstruction IS NULL OR @MoralInstruction = '' OR [result].[MoralInstruction] LIKE '%' + LTRIM(RTRIM(@MoralInstruction)) + '%') " 
                + "AND   (@TotalScore IS NULL OR @TotalScore = '' OR [result].[TotalScore] LIKE '%' + LTRIM(RTRIM(@TotalScore)) + '%') " 
                + "AND   (@Grade IS NULL OR @Grade = '' OR [result].[Grade] LIKE '%' + LTRIM(RTRIM(@Grade)) + '%') " 
                + "";
        } else if (sCondition == "Equals") {
            selectStatement
                = "SELECT "
            + "     [result].[ResultId] "
            + "    ,[A4].[ExamId] as [ExamId4]"
            + "    ,[A5].[StudentId] as [StudentId5]"
            + "    ,[result].[Mathematics] "
            + "    ,[result].[EnglishLanguage] "
            + "    ,[result].[QuantitativeReasoning] "
            + "    ,[result].[ElementaryScience] "
            + "    ,[result].[SocialStudies] "
            + "    ,[result].[VocationalAptitude] "
            + "    ,[result].[HealthEducation] "
            + "    ,[result].[Computer] "
            + "    ,[result].[HomeEconomics] "
            + "    ,[result].[MoralInstruction] "
            + "    ,[result].[TotalScore] "
            + "    ,[result].[Grade] "
            + "FROM " 
            + "     [result] " 
            + "LEFT JOIN [exam] as [A4] ON [result].[ExamId] = [A4].[ExamId] "
            + "LEFT JOIN [students] as [A5] ON [result].[StudentId] = [A5].[StudentId] "
                + "WHERE " 
                + "     (@ResultId IS NULL OR @ResultId = '' OR [result].[ResultId] = LTRIM(RTRIM(@ResultId))) " 
                + "AND   (@ExamId4 IS NULL OR @ExamId4 = '' OR [A4].[ExamId] = LTRIM(RTRIM(@ExamId4))) " 
                + "AND   (@StudentId5 IS NULL OR @StudentId5 = '' OR [A5].[StudentId] = LTRIM(RTRIM(@StudentId5))) " 
                + "AND   (@Mathematics IS NULL OR @Mathematics = '' OR [result].[Mathematics] = LTRIM(RTRIM(@Mathematics))) " 
                + "AND   (@EnglishLanguage IS NULL OR @EnglishLanguage = '' OR [result].[EnglishLanguage] = LTRIM(RTRIM(@EnglishLanguage))) " 
                + "AND   (@QuantitativeReasoning IS NULL OR @QuantitativeReasoning = '' OR [result].[QuantitativeReasoning] = LTRIM(RTRIM(@QuantitativeReasoning))) " 
                + "AND   (@ElementaryScience IS NULL OR @ElementaryScience = '' OR [result].[ElementaryScience] = LTRIM(RTRIM(@ElementaryScience))) " 
                + "AND   (@SocialStudies IS NULL OR @SocialStudies = '' OR [result].[SocialStudies] = LTRIM(RTRIM(@SocialStudies))) " 
                + "AND   (@VocationalAptitude IS NULL OR @VocationalAptitude = '' OR [result].[VocationalAptitude] = LTRIM(RTRIM(@VocationalAptitude))) " 
                + "AND   (@HealthEducation IS NULL OR @HealthEducation = '' OR [result].[HealthEducation] = LTRIM(RTRIM(@HealthEducation))) " 
                + "AND   (@Computer IS NULL OR @Computer = '' OR [result].[Computer] = LTRIM(RTRIM(@Computer))) " 
                + "AND   (@HomeEconomics IS NULL OR @HomeEconomics = '' OR [result].[HomeEconomics] = LTRIM(RTRIM(@HomeEconomics))) " 
                + "AND   (@MoralInstruction IS NULL OR @MoralInstruction = '' OR [result].[MoralInstruction] = LTRIM(RTRIM(@MoralInstruction))) " 
                + "AND   (@TotalScore IS NULL OR @TotalScore = '' OR [result].[TotalScore] = LTRIM(RTRIM(@TotalScore))) " 
                + "AND   (@Grade IS NULL OR @Grade = '' OR [result].[Grade] = LTRIM(RTRIM(@Grade))) " 
                + "";
        } else if  (sCondition == "Starts with...") {
            selectStatement
                = "SELECT "
            + "     [result].[ResultId] "
            + "    ,[A4].[ExamId] as [ExamId4]"
            + "    ,[A5].[StudentId] as [StudentId5]"
            + "    ,[result].[Mathematics] "
            + "    ,[result].[EnglishLanguage] "
            + "    ,[result].[QuantitativeReasoning] "
            + "    ,[result].[ElementaryScience] "
            + "    ,[result].[SocialStudies] "
            + "    ,[result].[VocationalAptitude] "
            + "    ,[result].[HealthEducation] "
            + "    ,[result].[Computer] "
            + "    ,[result].[HomeEconomics] "
            + "    ,[result].[MoralInstruction] "
            + "    ,[result].[TotalScore] "
            + "    ,[result].[Grade] "
            + "FROM " 
            + "     [result] " 
            + "LEFT JOIN [exam] as [A4] ON [result].[ExamId] = [A4].[ExamId] "
            + "LEFT JOIN [students] as [A5] ON [result].[StudentId] = [A5].[StudentId] "
                + "WHERE " 
                + "     (@ResultId IS NULL OR @ResultId = '' OR [result].[ResultId] LIKE LTRIM(RTRIM(@ResultId)) + '%') " 
                + "AND   (@ExamId4 IS NULL OR @ExamId4 = '' OR [A4].[ExamId] LIKE LTRIM(RTRIM(@ExamId4)) + '%') " 
                + "AND   (@StudentId5 IS NULL OR @StudentId5 = '' OR [A5].[StudentId] LIKE LTRIM(RTRIM(@StudentId5)) + '%') " 
                + "AND   (@Mathematics IS NULL OR @Mathematics = '' OR [result].[Mathematics] LIKE LTRIM(RTRIM(@Mathematics)) + '%') " 
                + "AND   (@EnglishLanguage IS NULL OR @EnglishLanguage = '' OR [result].[EnglishLanguage] LIKE LTRIM(RTRIM(@EnglishLanguage)) + '%') " 
                + "AND   (@QuantitativeReasoning IS NULL OR @QuantitativeReasoning = '' OR [result].[QuantitativeReasoning] LIKE LTRIM(RTRIM(@QuantitativeReasoning)) + '%') " 
                + "AND   (@ElementaryScience IS NULL OR @ElementaryScience = '' OR [result].[ElementaryScience] LIKE LTRIM(RTRIM(@ElementaryScience)) + '%') " 
                + "AND   (@SocialStudies IS NULL OR @SocialStudies = '' OR [result].[SocialStudies] LIKE LTRIM(RTRIM(@SocialStudies)) + '%') " 
                + "AND   (@VocationalAptitude IS NULL OR @VocationalAptitude = '' OR [result].[VocationalAptitude] LIKE LTRIM(RTRIM(@VocationalAptitude)) + '%') " 
                + "AND   (@HealthEducation IS NULL OR @HealthEducation = '' OR [result].[HealthEducation] LIKE LTRIM(RTRIM(@HealthEducation)) + '%') " 
                + "AND   (@Computer IS NULL OR @Computer = '' OR [result].[Computer] LIKE LTRIM(RTRIM(@Computer)) + '%') " 
                + "AND   (@HomeEconomics IS NULL OR @HomeEconomics = '' OR [result].[HomeEconomics] LIKE LTRIM(RTRIM(@HomeEconomics)) + '%') " 
                + "AND   (@MoralInstruction IS NULL OR @MoralInstruction = '' OR [result].[MoralInstruction] LIKE LTRIM(RTRIM(@MoralInstruction)) + '%') " 
                + "AND   (@TotalScore IS NULL OR @TotalScore = '' OR [result].[TotalScore] LIKE LTRIM(RTRIM(@TotalScore)) + '%') " 
                + "AND   (@Grade IS NULL OR @Grade = '' OR [result].[Grade] LIKE LTRIM(RTRIM(@Grade)) + '%') " 
                + "";
        } else if  (sCondition == "More than...") {
            selectStatement
                = "SELECT "
            + "     [result].[ResultId] "
            + "    ,[A4].[ExamId] as [ExamId4]"
            + "    ,[A5].[StudentId] as [StudentId5]"
            + "    ,[result].[Mathematics] "
            + "    ,[result].[EnglishLanguage] "
            + "    ,[result].[QuantitativeReasoning] "
            + "    ,[result].[ElementaryScience] "
            + "    ,[result].[SocialStudies] "
            + "    ,[result].[VocationalAptitude] "
            + "    ,[result].[HealthEducation] "
            + "    ,[result].[Computer] "
            + "    ,[result].[HomeEconomics] "
            + "    ,[result].[MoralInstruction] "
            + "    ,[result].[TotalScore] "
            + "    ,[result].[Grade] "
            + "FROM " 
            + "     [result] " 
            + "LEFT JOIN [exam] as [A4] ON [result].[ExamId] = [A4].[ExamId] "
            + "LEFT JOIN [students] as [A5] ON [result].[StudentId] = [A5].[StudentId] "
                + "WHERE " 
                + "     (@ResultId IS NULL OR @ResultId = '' OR [result].[ResultId] > LTRIM(RTRIM(@ResultId))) " 
                + "AND   (@ExamId4 IS NULL OR @ExamId4 = '' OR [A4].[ExamId] > LTRIM(RTRIM(@ExamId4))) " 
                + "AND   (@StudentId5 IS NULL OR @StudentId5 = '' OR [A5].[StudentId] > LTRIM(RTRIM(@StudentId5))) " 
                + "AND   (@Mathematics IS NULL OR @Mathematics = '' OR [result].[Mathematics] > LTRIM(RTRIM(@Mathematics))) " 
                + "AND   (@EnglishLanguage IS NULL OR @EnglishLanguage = '' OR [result].[EnglishLanguage] > LTRIM(RTRIM(@EnglishLanguage))) " 
                + "AND   (@QuantitativeReasoning IS NULL OR @QuantitativeReasoning = '' OR [result].[QuantitativeReasoning] > LTRIM(RTRIM(@QuantitativeReasoning))) " 
                + "AND   (@ElementaryScience IS NULL OR @ElementaryScience = '' OR [result].[ElementaryScience] > LTRIM(RTRIM(@ElementaryScience))) " 
                + "AND   (@SocialStudies IS NULL OR @SocialStudies = '' OR [result].[SocialStudies] > LTRIM(RTRIM(@SocialStudies))) " 
                + "AND   (@VocationalAptitude IS NULL OR @VocationalAptitude = '' OR [result].[VocationalAptitude] > LTRIM(RTRIM(@VocationalAptitude))) " 
                + "AND   (@HealthEducation IS NULL OR @HealthEducation = '' OR [result].[HealthEducation] > LTRIM(RTRIM(@HealthEducation))) " 
                + "AND   (@Computer IS NULL OR @Computer = '' OR [result].[Computer] > LTRIM(RTRIM(@Computer))) " 
                + "AND   (@HomeEconomics IS NULL OR @HomeEconomics = '' OR [result].[HomeEconomics] > LTRIM(RTRIM(@HomeEconomics))) " 
                + "AND   (@MoralInstruction IS NULL OR @MoralInstruction = '' OR [result].[MoralInstruction] > LTRIM(RTRIM(@MoralInstruction))) " 
                + "AND   (@TotalScore IS NULL OR @TotalScore = '' OR [result].[TotalScore] > LTRIM(RTRIM(@TotalScore))) " 
                + "AND   (@Grade IS NULL OR @Grade = '' OR [result].[Grade] > LTRIM(RTRIM(@Grade))) " 
                + "";
        } else if  (sCondition == "Less than...") {
            selectStatement
                = "SELECT " 
            + "     [result].[ResultId] "
            + "    ,[A4].[ExamId] as [ExamId4]"
            + "    ,[A5].[StudentId] as [StudentId5]"
            + "    ,[result].[Mathematics] "
            + "    ,[result].[EnglishLanguage] "
            + "    ,[result].[QuantitativeReasoning] "
            + "    ,[result].[ElementaryScience] "
            + "    ,[result].[SocialStudies] "
            + "    ,[result].[VocationalAptitude] "
            + "    ,[result].[HealthEducation] "
            + "    ,[result].[Computer] "
            + "    ,[result].[HomeEconomics] "
            + "    ,[result].[MoralInstruction] "
            + "    ,[result].[TotalScore] "
            + "    ,[result].[Grade] "
            + "FROM " 
            + "     [result] " 
            + "LEFT JOIN [exam] as [A4] ON [result].[ExamId] = [A4].[ExamId] "
            + "LEFT JOIN [students] as [A5] ON [result].[StudentId] = [A5].[StudentId] "
                + "WHERE " 
                + "     (@ResultId IS NULL OR @ResultId = '' OR [result].[ResultId] < LTRIM(RTRIM(@ResultId))) " 
                + "AND   (@ExamId4 IS NULL OR @ExamId4 = '' OR [A4].[ExamId] < LTRIM(RTRIM(@ExamId4))) " 
                + "AND   (@StudentId5 IS NULL OR @StudentId5 = '' OR [A5].[StudentId] < LTRIM(RTRIM(@StudentId5))) " 
                + "AND   (@Mathematics IS NULL OR @Mathematics = '' OR [result].[Mathematics] < LTRIM(RTRIM(@Mathematics))) " 
                + "AND   (@EnglishLanguage IS NULL OR @EnglishLanguage = '' OR [result].[EnglishLanguage] < LTRIM(RTRIM(@EnglishLanguage))) " 
                + "AND   (@QuantitativeReasoning IS NULL OR @QuantitativeReasoning = '' OR [result].[QuantitativeReasoning] < LTRIM(RTRIM(@QuantitativeReasoning))) " 
                + "AND   (@ElementaryScience IS NULL OR @ElementaryScience = '' OR [result].[ElementaryScience] < LTRIM(RTRIM(@ElementaryScience))) " 
                + "AND   (@SocialStudies IS NULL OR @SocialStudies = '' OR [result].[SocialStudies] < LTRIM(RTRIM(@SocialStudies))) " 
                + "AND   (@VocationalAptitude IS NULL OR @VocationalAptitude = '' OR [result].[VocationalAptitude] < LTRIM(RTRIM(@VocationalAptitude))) " 
                + "AND   (@HealthEducation IS NULL OR @HealthEducation = '' OR [result].[HealthEducation] < LTRIM(RTRIM(@HealthEducation))) " 
                + "AND   (@Computer IS NULL OR @Computer = '' OR [result].[Computer] < LTRIM(RTRIM(@Computer))) " 
                + "AND   (@HomeEconomics IS NULL OR @HomeEconomics = '' OR [result].[HomeEconomics] < LTRIM(RTRIM(@HomeEconomics))) " 
                + "AND   (@MoralInstruction IS NULL OR @MoralInstruction = '' OR [result].[MoralInstruction] < LTRIM(RTRIM(@MoralInstruction))) " 
                + "AND   (@TotalScore IS NULL OR @TotalScore = '' OR [result].[TotalScore] < LTRIM(RTRIM(@TotalScore))) " 
                + "AND   (@Grade IS NULL OR @Grade = '' OR [result].[Grade] < LTRIM(RTRIM(@Grade))) " 
                + "";
        } else if  (sCondition == "Equal or more than...") {
            selectStatement
                = "SELECT "
            + "     [result].[ResultId] "
            + "    ,[A4].[ExamId] as [ExamId4]"
            + "    ,[A5].[StudentId] as [StudentId5]"
            + "    ,[result].[Mathematics] "
            + "    ,[result].[EnglishLanguage] "
            + "    ,[result].[QuantitativeReasoning] "
            + "    ,[result].[ElementaryScience] "
            + "    ,[result].[SocialStudies] "
            + "    ,[result].[VocationalAptitude] "
            + "    ,[result].[HealthEducation] "
            + "    ,[result].[Computer] "
            + "    ,[result].[HomeEconomics] "
            + "    ,[result].[MoralInstruction] "
            + "    ,[result].[TotalScore] "
            + "    ,[result].[Grade] "
            + "FROM " 
            + "     [result] " 
            + "LEFT JOIN [exam] as [A4] ON [result].[ExamId] = [A4].[ExamId] "
            + "LEFT JOIN [students] as [A5] ON [result].[StudentId] = [A5].[StudentId] "
                + "WHERE " 
                + "     (@ResultId IS NULL OR @ResultId = '' OR [result].[ResultId] >= LTRIM(RTRIM(@ResultId))) " 
                + "AND   (@ExamId4 IS NULL OR @ExamId4 = '' OR [A4].[ExamId] >= LTRIM(RTRIM(@ExamId4))) " 
                + "AND   (@StudentId5 IS NULL OR @StudentId5 = '' OR [A5].[StudentId] >= LTRIM(RTRIM(@StudentId5))) " 
                + "AND   (@Mathematics IS NULL OR @Mathematics = '' OR [result].[Mathematics] >= LTRIM(RTRIM(@Mathematics))) " 
                + "AND   (@EnglishLanguage IS NULL OR @EnglishLanguage = '' OR [result].[EnglishLanguage] >= LTRIM(RTRIM(@EnglishLanguage))) " 
                + "AND   (@QuantitativeReasoning IS NULL OR @QuantitativeReasoning = '' OR [result].[QuantitativeReasoning] >= LTRIM(RTRIM(@QuantitativeReasoning))) " 
                + "AND   (@ElementaryScience IS NULL OR @ElementaryScience = '' OR [result].[ElementaryScience] >= LTRIM(RTRIM(@ElementaryScience))) " 
                + "AND   (@SocialStudies IS NULL OR @SocialStudies = '' OR [result].[SocialStudies] >= LTRIM(RTRIM(@SocialStudies))) " 
                + "AND   (@VocationalAptitude IS NULL OR @VocationalAptitude = '' OR [result].[VocationalAptitude] >= LTRIM(RTRIM(@VocationalAptitude))) " 
                + "AND   (@HealthEducation IS NULL OR @HealthEducation = '' OR [result].[HealthEducation] >= LTRIM(RTRIM(@HealthEducation))) " 
                + "AND   (@Computer IS NULL OR @Computer = '' OR [result].[Computer] >= LTRIM(RTRIM(@Computer))) " 
                + "AND   (@HomeEconomics IS NULL OR @HomeEconomics = '' OR [result].[HomeEconomics] >= LTRIM(RTRIM(@HomeEconomics))) " 
                + "AND   (@MoralInstruction IS NULL OR @MoralInstruction = '' OR [result].[MoralInstruction] >= LTRIM(RTRIM(@MoralInstruction))) " 
                + "AND   (@TotalScore IS NULL OR @TotalScore = '' OR [result].[TotalScore] >= LTRIM(RTRIM(@TotalScore))) " 
                + "AND   (@Grade IS NULL OR @Grade = '' OR [result].[Grade] >= LTRIM(RTRIM(@Grade))) " 
                + "";
        } else if (sCondition == "Equal or less than...") {
            selectStatement 
                = "SELECT "
            + "     [result].[ResultId] "
            + "    ,[A4].[ExamId] as [ExamId4]"
            + "    ,[A5].[StudentId] as [StudentId5]"
            + "    ,[result].[Mathematics] "
            + "    ,[result].[EnglishLanguage] "
            + "    ,[result].[QuantitativeReasoning] "
            + "    ,[result].[ElementaryScience] "
            + "    ,[result].[SocialStudies] "
            + "    ,[result].[VocationalAptitude] "
            + "    ,[result].[HealthEducation] "
            + "    ,[result].[Computer] "
            + "    ,[result].[HomeEconomics] "
            + "    ,[result].[MoralInstruction] "
            + "    ,[result].[TotalScore] "
            + "    ,[result].[Grade] "
            + "FROM " 
            + "     [result] " 
            + "LEFT JOIN [exam] as [A4] ON [result].[ExamId] = [A4].[ExamId] "
            + "LEFT JOIN [students] as [A5] ON [result].[StudentId] = [A5].[StudentId] "
                + "WHERE " 
                + "     (@ResultId IS NULL OR @ResultId = '' OR [result].[ResultId] <= LTRIM(RTRIM(@ResultId))) " 
                + "AND   (@ExamId4 IS NULL OR @ExamId4 = '' OR [A4].[ExamId] <= LTRIM(RTRIM(@ExamId4))) " 
                + "AND   (@StudentId5 IS NULL OR @StudentId5 = '' OR [A5].[StudentId] <= LTRIM(RTRIM(@StudentId5))) " 
                + "AND   (@Mathematics IS NULL OR @Mathematics = '' OR [result].[Mathematics] <= LTRIM(RTRIM(@Mathematics))) " 
                + "AND   (@EnglishLanguage IS NULL OR @EnglishLanguage = '' OR [result].[EnglishLanguage] <= LTRIM(RTRIM(@EnglishLanguage))) " 
                + "AND   (@QuantitativeReasoning IS NULL OR @QuantitativeReasoning = '' OR [result].[QuantitativeReasoning] <= LTRIM(RTRIM(@QuantitativeReasoning))) " 
                + "AND   (@ElementaryScience IS NULL OR @ElementaryScience = '' OR [result].[ElementaryScience] <= LTRIM(RTRIM(@ElementaryScience))) " 
                + "AND   (@SocialStudies IS NULL OR @SocialStudies = '' OR [result].[SocialStudies] <= LTRIM(RTRIM(@SocialStudies))) " 
                + "AND   (@VocationalAptitude IS NULL OR @VocationalAptitude = '' OR [result].[VocationalAptitude] <= LTRIM(RTRIM(@VocationalAptitude))) " 
                + "AND   (@HealthEducation IS NULL OR @HealthEducation = '' OR [result].[HealthEducation] <= LTRIM(RTRIM(@HealthEducation))) " 
                + "AND   (@Computer IS NULL OR @Computer = '' OR [result].[Computer] <= LTRIM(RTRIM(@Computer))) " 
                + "AND   (@HomeEconomics IS NULL OR @HomeEconomics = '' OR [result].[HomeEconomics] <= LTRIM(RTRIM(@HomeEconomics))) " 
                + "AND   (@MoralInstruction IS NULL OR @MoralInstruction = '' OR [result].[MoralInstruction] <= LTRIM(RTRIM(@MoralInstruction))) " 
                + "AND   (@TotalScore IS NULL OR @TotalScore = '' OR [result].[TotalScore] <= LTRIM(RTRIM(@TotalScore))) " 
                + "AND   (@Grade IS NULL OR @Grade = '' OR [result].[Grade] <= LTRIM(RTRIM(@Grade))) " 
                + "";
        }
        SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
        selectCommand.CommandType = CommandType.Text;
        if (sField == "Result ID") {
            selectCommand.Parameters.AddWithValue("@ResultId", sValue);
        } else {
            selectCommand.Parameters.AddWithValue("@ResultId", DBNull.Value); }
        if (sField == "Exam ID") {
            selectCommand.Parameters.AddWithValue("@ExamId4", sValue);
        } else {
            selectCommand.Parameters.AddWithValue("@ExamId4", DBNull.Value); }
        if (sField == "Student ID") {
            selectCommand.Parameters.AddWithValue("@StudentId5", sValue);
        } else {
            selectCommand.Parameters.AddWithValue("@StudentId5", DBNull.Value); }
        if (sField == "Mathematics") {
            selectCommand.Parameters.AddWithValue("@Mathematics", sValue);
        } else {
            selectCommand.Parameters.AddWithValue("@Mathematics", DBNull.Value); }
        if (sField == "English Language") {
            selectCommand.Parameters.AddWithValue("@EnglishLanguage", sValue);
        } else {
            selectCommand.Parameters.AddWithValue("@EnglishLanguage", DBNull.Value); }
        if (sField == "Quantitative Reasoning") {
            selectCommand.Parameters.AddWithValue("@QuantitativeReasoning", sValue);
        } else {
            selectCommand.Parameters.AddWithValue("@QuantitativeReasoning", DBNull.Value); }
        if (sField == "Elementary Science") {
            selectCommand.Parameters.AddWithValue("@ElementaryScience", sValue);
        } else {
            selectCommand.Parameters.AddWithValue("@ElementaryScience", DBNull.Value); }
        if (sField == "Social Studies") {
            selectCommand.Parameters.AddWithValue("@SocialStudies", sValue);
        } else {
            selectCommand.Parameters.AddWithValue("@SocialStudies", DBNull.Value); }
        if (sField == "Vocational Aptitude") {
            selectCommand.Parameters.AddWithValue("@VocationalAptitude", sValue);
        } else {
            selectCommand.Parameters.AddWithValue("@VocationalAptitude", DBNull.Value); }
        if (sField == "Health Education") {
            selectCommand.Parameters.AddWithValue("@HealthEducation", sValue);
        } else {
            selectCommand.Parameters.AddWithValue("@HealthEducation", DBNull.Value); }
        if (sField == "Computer") {
            selectCommand.Parameters.AddWithValue("@Computer", sValue);
        } else {
            selectCommand.Parameters.AddWithValue("@Computer", DBNull.Value); }
        if (sField == "Home Economics") {
            selectCommand.Parameters.AddWithValue("@HomeEconomics", sValue);
        } else {
            selectCommand.Parameters.AddWithValue("@HomeEconomics", DBNull.Value); }
        if (sField == "Moral Instruction") {
            selectCommand.Parameters.AddWithValue("@MoralInstruction", sValue);
        } else {
            selectCommand.Parameters.AddWithValue("@MoralInstruction", DBNull.Value); }
        if (sField == "Total Score") {
            selectCommand.Parameters.AddWithValue("@TotalScore", sValue);
        } else {
            selectCommand.Parameters.AddWithValue("@TotalScore", DBNull.Value); }
        if (sField == "Grade") {
            selectCommand.Parameters.AddWithValue("@Grade", sValue);
        } else {
            selectCommand.Parameters.AddWithValue("@Grade", DBNull.Value); }
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

    public static result Select_Record(result clsresultPara)
    {
        result clsresult = new result();
        SqlConnection connection = SchoolData.GetConnection();
        string selectStatement
            = "SELECT " 
            + "     [ResultId] "
            + "    ,[ExamId] "
            + "    ,[StudentId] "
            + "    ,[Mathematics] "
            + "    ,[EnglishLanguage] "
            + "    ,[QuantitativeReasoning] "
            + "    ,[ElementaryScience] "
            + "    ,[SocialStudies] "
            + "    ,[VocationalAptitude] "
            + "    ,[HealthEducation] "
            + "    ,[Computer] "
            + "    ,[HomeEconomics] "
            + "    ,[MoralInstruction] "
            + "    ,[TotalScore] "
            + "    ,[Grade] "
            + "FROM "
            + "     [result] "
            + "WHERE "
            + "     [ResultId] = @ResultId "
            + "";
        SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
        selectCommand.CommandType = CommandType.Text;
        selectCommand.Parameters.AddWithValue("@ResultId", clsresultPara.ResultId);
        try
        {
            connection.Open();
            SqlDataReader reader
                = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
                clsresult.ResultId = System.Convert.ToInt32(reader["ResultId"]);
                clsresult.ExamId = reader["ExamId"] is DBNull ? null : (Int32?)reader["ExamId"];
                clsresult.StudentId = reader["StudentId"] is DBNull ? null : (Int32?)reader["StudentId"];
                clsresult.Mathematics = reader["Mathematics"] is DBNull ? null : reader["Mathematics"].ToString();
                clsresult.EnglishLanguage = reader["EnglishLanguage"] is DBNull ? null : reader["EnglishLanguage"].ToString();
                clsresult.QuantitativeReasoning = reader["QuantitativeReasoning"] is DBNull ? null : reader["QuantitativeReasoning"].ToString();
                clsresult.ElementaryScience = reader["ElementaryScience"] is DBNull ? null : reader["ElementaryScience"].ToString();
                clsresult.SocialStudies = reader["SocialStudies"] is DBNull ? null : reader["SocialStudies"].ToString();
                clsresult.VocationalAptitude = reader["VocationalAptitude"] is DBNull ? null : reader["VocationalAptitude"].ToString();
                clsresult.HealthEducation = reader["HealthEducation"] is DBNull ? null : reader["HealthEducation"].ToString();
                clsresult.Computer = reader["Computer"] is DBNull ? null : reader["Computer"].ToString();
                clsresult.HomeEconomics = reader["HomeEconomics"] is DBNull ? null : reader["HomeEconomics"].ToString();
                clsresult.MoralInstruction = reader["MoralInstruction"] is DBNull ? null : reader["MoralInstruction"].ToString();
                clsresult.TotalScore = reader["TotalScore"] is DBNull ? null : (Int64?)reader["TotalScore"];
                clsresult.Grade = reader["Grade"] is DBNull ? null : reader["Grade"].ToString();
            }
            else
            {
                clsresult = null;
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
        return clsresult;
    }

    public static bool Add(result clsresult)
    {
        SqlConnection connection = SchoolData.GetConnection();
        string insertStatement
            = "INSERT " 
            + "     [result] "
            + "     ( "
            + "     [ExamId] "
            + "    ,[StudentId] "
            + "    ,[Mathematics] "
            + "    ,[EnglishLanguage] "
            + "    ,[QuantitativeReasoning] "
            + "    ,[ElementaryScience] "
            + "    ,[SocialStudies] "
            + "    ,[VocationalAptitude] "
            + "    ,[HealthEducation] "
            + "    ,[Computer] "
            + "    ,[HomeEconomics] "
            + "    ,[MoralInstruction] "
            + "    ,[TotalScore] "
            + "    ,[Grade] "
            + "     ) "
            + "VALUES " 
            + "     ( "
            + "     @ExamId "
            + "    ,@StudentId "
            + "    ,@Mathematics "
            + "    ,@EnglishLanguage "
            + "    ,@QuantitativeReasoning "
            + "    ,@ElementaryScience "
            + "    ,@SocialStudies "
            + "    ,@VocationalAptitude "
            + "    ,@HealthEducation "
            + "    ,@Computer "
            + "    ,@HomeEconomics "
            + "    ,@MoralInstruction "
            + "    ,@TotalScore "
            + "    ,@Grade "
            + "     ) "
            + "";
        SqlCommand insertCommand = new SqlCommand(insertStatement, connection);
        insertCommand.CommandType = CommandType.Text;
        if (clsresult.ExamId.HasValue == true) {
            insertCommand.Parameters.AddWithValue("@ExamId", clsresult.ExamId);
        } else {
            insertCommand.Parameters.AddWithValue("@ExamId", DBNull.Value); }
        if (clsresult.StudentId.HasValue == true) {
            insertCommand.Parameters.AddWithValue("@StudentId", clsresult.StudentId);
        } else {
            insertCommand.Parameters.AddWithValue("@StudentId", DBNull.Value); }
        if (clsresult.Mathematics != null) {
            insertCommand.Parameters.AddWithValue("@Mathematics", clsresult.Mathematics);
        } else {
            insertCommand.Parameters.AddWithValue("@Mathematics", DBNull.Value); }
        if (clsresult.EnglishLanguage != null) {
            insertCommand.Parameters.AddWithValue("@EnglishLanguage", clsresult.EnglishLanguage);
        } else {
            insertCommand.Parameters.AddWithValue("@EnglishLanguage", DBNull.Value); }
        if (clsresult.QuantitativeReasoning != null) {
            insertCommand.Parameters.AddWithValue("@QuantitativeReasoning", clsresult.QuantitativeReasoning);
        } else {
            insertCommand.Parameters.AddWithValue("@QuantitativeReasoning", DBNull.Value); }
        if (clsresult.ElementaryScience != null) {
            insertCommand.Parameters.AddWithValue("@ElementaryScience", clsresult.ElementaryScience);
        } else {
            insertCommand.Parameters.AddWithValue("@ElementaryScience", DBNull.Value); }
        if (clsresult.SocialStudies != null) {
            insertCommand.Parameters.AddWithValue("@SocialStudies", clsresult.SocialStudies);
        } else {
            insertCommand.Parameters.AddWithValue("@SocialStudies", DBNull.Value); }
        if (clsresult.VocationalAptitude != null) {
            insertCommand.Parameters.AddWithValue("@VocationalAptitude", clsresult.VocationalAptitude);
        } else {
            insertCommand.Parameters.AddWithValue("@VocationalAptitude", DBNull.Value); }
        if (clsresult.HealthEducation != null) {
            insertCommand.Parameters.AddWithValue("@HealthEducation", clsresult.HealthEducation);
        } else {
            insertCommand.Parameters.AddWithValue("@HealthEducation", DBNull.Value); }
        if (clsresult.Computer != null) {
            insertCommand.Parameters.AddWithValue("@Computer", clsresult.Computer);
        } else {
            insertCommand.Parameters.AddWithValue("@Computer", DBNull.Value); }
        if (clsresult.HomeEconomics != null) {
            insertCommand.Parameters.AddWithValue("@HomeEconomics", clsresult.HomeEconomics);
        } else {
            insertCommand.Parameters.AddWithValue("@HomeEconomics", DBNull.Value); }
        if (clsresult.MoralInstruction != null) {
            insertCommand.Parameters.AddWithValue("@MoralInstruction", clsresult.MoralInstruction);
        } else {
            insertCommand.Parameters.AddWithValue("@MoralInstruction", DBNull.Value); }
        if (clsresult.TotalScore.HasValue == true) {
            insertCommand.Parameters.AddWithValue("@TotalScore", clsresult.TotalScore);
        } else {
            insertCommand.Parameters.AddWithValue("@TotalScore", DBNull.Value); }
        if (clsresult.Grade != null) {
            insertCommand.Parameters.AddWithValue("@Grade", clsresult.Grade);
        } else {
            insertCommand.Parameters.AddWithValue("@Grade", DBNull.Value); }
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

    public static bool Update(result oldresult, 
           result newresult)
    {
        SqlConnection connection = SchoolData.GetConnection();
        string updateStatement
            = "UPDATE "  
            + "     [result] "
            + "SET "
            + "     [ExamId] = @NewExamId "
            + "    ,[StudentId] = @NewStudentId "
            + "    ,[Mathematics] = @NewMathematics "
            + "    ,[EnglishLanguage] = @NewEnglishLanguage "
            + "    ,[QuantitativeReasoning] = @NewQuantitativeReasoning "
            + "    ,[ElementaryScience] = @NewElementaryScience "
            + "    ,[SocialStudies] = @NewSocialStudies "
            + "    ,[VocationalAptitude] = @NewVocationalAptitude "
            + "    ,[HealthEducation] = @NewHealthEducation "
            + "    ,[Computer] = @NewComputer "
            + "    ,[HomeEconomics] = @NewHomeEconomics "
            + "    ,[MoralInstruction] = @NewMoralInstruction "
            + "    ,[TotalScore] = @NewTotalScore "
            + "    ,[Grade] = @NewGrade "
            + "WHERE "
            + "     [ResultId] = @OldResultId " 
            + " AND ((@OldExamId IS NULL AND [ExamId] IS NULL) OR [ExamId] = @OldExamId) " 
            + " AND ((@OldStudentId IS NULL AND [StudentId] IS NULL) OR [StudentId] = @OldStudentId) " 
            + " AND ((@OldMathematics IS NULL AND [Mathematics] IS NULL) OR [Mathematics] = @OldMathematics) " 
            + " AND ((@OldEnglishLanguage IS NULL AND [EnglishLanguage] IS NULL) OR [EnglishLanguage] = @OldEnglishLanguage) " 
            + " AND ((@OldQuantitativeReasoning IS NULL AND [QuantitativeReasoning] IS NULL) OR [QuantitativeReasoning] = @OldQuantitativeReasoning) " 
            + " AND ((@OldElementaryScience IS NULL AND [ElementaryScience] IS NULL) OR [ElementaryScience] = @OldElementaryScience) " 
            + " AND ((@OldSocialStudies IS NULL AND [SocialStudies] IS NULL) OR [SocialStudies] = @OldSocialStudies) " 
            + " AND ((@OldVocationalAptitude IS NULL AND [VocationalAptitude] IS NULL) OR [VocationalAptitude] = @OldVocationalAptitude) " 
            + " AND ((@OldHealthEducation IS NULL AND [HealthEducation] IS NULL) OR [HealthEducation] = @OldHealthEducation) " 
            + " AND ((@OldComputer IS NULL AND [Computer] IS NULL) OR [Computer] = @OldComputer) " 
            + " AND ((@OldHomeEconomics IS NULL AND [HomeEconomics] IS NULL) OR [HomeEconomics] = @OldHomeEconomics) " 
            + " AND ((@OldMoralInstruction IS NULL AND [MoralInstruction] IS NULL) OR [MoralInstruction] = @OldMoralInstruction) " 
            + " AND ((@OldTotalScore IS NULL AND [TotalScore] IS NULL) OR [TotalScore] = @OldTotalScore) " 
            + " AND ((@OldGrade IS NULL AND [Grade] IS NULL) OR [Grade] = @OldGrade) " 
            + "";
        SqlCommand updateCommand = new SqlCommand(updateStatement, connection);
        updateCommand.CommandType = CommandType.Text;
        if (newresult.ExamId.HasValue == true) {
            updateCommand.Parameters.AddWithValue("@NewExamId", newresult.ExamId);
        } else {
            updateCommand.Parameters.AddWithValue("@NewExamId", DBNull.Value); }
        if (newresult.StudentId.HasValue == true) {
            updateCommand.Parameters.AddWithValue("@NewStudentId", newresult.StudentId);
        } else {
            updateCommand.Parameters.AddWithValue("@NewStudentId", DBNull.Value); }
        if (newresult.Mathematics != null) {
            updateCommand.Parameters.AddWithValue("@NewMathematics", newresult.Mathematics);
        } else {
            updateCommand.Parameters.AddWithValue("@NewMathematics", DBNull.Value); }
        if (newresult.EnglishLanguage != null) {
            updateCommand.Parameters.AddWithValue("@NewEnglishLanguage", newresult.EnglishLanguage);
        } else {
            updateCommand.Parameters.AddWithValue("@NewEnglishLanguage", DBNull.Value); }
        if (newresult.QuantitativeReasoning != null) {
            updateCommand.Parameters.AddWithValue("@NewQuantitativeReasoning", newresult.QuantitativeReasoning);
        } else {
            updateCommand.Parameters.AddWithValue("@NewQuantitativeReasoning", DBNull.Value); }
        if (newresult.ElementaryScience != null) {
            updateCommand.Parameters.AddWithValue("@NewElementaryScience", newresult.ElementaryScience);
        } else {
            updateCommand.Parameters.AddWithValue("@NewElementaryScience", DBNull.Value); }
        if (newresult.SocialStudies != null) {
            updateCommand.Parameters.AddWithValue("@NewSocialStudies", newresult.SocialStudies);
        } else {
            updateCommand.Parameters.AddWithValue("@NewSocialStudies", DBNull.Value); }
        if (newresult.VocationalAptitude != null) {
            updateCommand.Parameters.AddWithValue("@NewVocationalAptitude", newresult.VocationalAptitude);
        } else {
            updateCommand.Parameters.AddWithValue("@NewVocationalAptitude", DBNull.Value); }
        if (newresult.HealthEducation != null) {
            updateCommand.Parameters.AddWithValue("@NewHealthEducation", newresult.HealthEducation);
        } else {
            updateCommand.Parameters.AddWithValue("@NewHealthEducation", DBNull.Value); }
        if (newresult.Computer != null) {
            updateCommand.Parameters.AddWithValue("@NewComputer", newresult.Computer);
        } else {
            updateCommand.Parameters.AddWithValue("@NewComputer", DBNull.Value); }
        if (newresult.HomeEconomics != null) {
            updateCommand.Parameters.AddWithValue("@NewHomeEconomics", newresult.HomeEconomics);
        } else {
            updateCommand.Parameters.AddWithValue("@NewHomeEconomics", DBNull.Value); }
        if (newresult.MoralInstruction != null) {
            updateCommand.Parameters.AddWithValue("@NewMoralInstruction", newresult.MoralInstruction);
        } else {
            updateCommand.Parameters.AddWithValue("@NewMoralInstruction", DBNull.Value); }
        if (newresult.TotalScore.HasValue == true) {
            updateCommand.Parameters.AddWithValue("@NewTotalScore", newresult.TotalScore);
        } else {
            updateCommand.Parameters.AddWithValue("@NewTotalScore", DBNull.Value); }
        if (newresult.Grade != null) {
            updateCommand.Parameters.AddWithValue("@NewGrade", newresult.Grade);
        } else {
            updateCommand.Parameters.AddWithValue("@NewGrade", DBNull.Value); }
        updateCommand.Parameters.AddWithValue("@OldResultId", oldresult.ResultId);
        if (oldresult.ExamId.HasValue == true) {
            updateCommand.Parameters.AddWithValue("@OldExamId", oldresult.ExamId);
        } else {
            updateCommand.Parameters.AddWithValue("@OldExamId", DBNull.Value); }
        if (oldresult.StudentId.HasValue == true) {
            updateCommand.Parameters.AddWithValue("@OldStudentId", oldresult.StudentId);
        } else {
            updateCommand.Parameters.AddWithValue("@OldStudentId", DBNull.Value); }
        if (oldresult.Mathematics != null) {
            updateCommand.Parameters.AddWithValue("@OldMathematics", oldresult.Mathematics);
        } else {
            updateCommand.Parameters.AddWithValue("@OldMathematics", DBNull.Value); }
        if (oldresult.EnglishLanguage != null) {
            updateCommand.Parameters.AddWithValue("@OldEnglishLanguage", oldresult.EnglishLanguage);
        } else {
            updateCommand.Parameters.AddWithValue("@OldEnglishLanguage", DBNull.Value); }
        if (oldresult.QuantitativeReasoning != null) {
            updateCommand.Parameters.AddWithValue("@OldQuantitativeReasoning", oldresult.QuantitativeReasoning);
        } else {
            updateCommand.Parameters.AddWithValue("@OldQuantitativeReasoning", DBNull.Value); }
        if (oldresult.ElementaryScience != null) {
            updateCommand.Parameters.AddWithValue("@OldElementaryScience", oldresult.ElementaryScience);
        } else {
            updateCommand.Parameters.AddWithValue("@OldElementaryScience", DBNull.Value); }
        if (oldresult.SocialStudies != null) {
            updateCommand.Parameters.AddWithValue("@OldSocialStudies", oldresult.SocialStudies);
        } else {
            updateCommand.Parameters.AddWithValue("@OldSocialStudies", DBNull.Value); }
        if (oldresult.VocationalAptitude != null) {
            updateCommand.Parameters.AddWithValue("@OldVocationalAptitude", oldresult.VocationalAptitude);
        } else {
            updateCommand.Parameters.AddWithValue("@OldVocationalAptitude", DBNull.Value); }
        if (oldresult.HealthEducation != null) {
            updateCommand.Parameters.AddWithValue("@OldHealthEducation", oldresult.HealthEducation);
        } else {
            updateCommand.Parameters.AddWithValue("@OldHealthEducation", DBNull.Value); }
        if (oldresult.Computer != null) {
            updateCommand.Parameters.AddWithValue("@OldComputer", oldresult.Computer);
        } else {
            updateCommand.Parameters.AddWithValue("@OldComputer", DBNull.Value); }
        if (oldresult.HomeEconomics != null) {
            updateCommand.Parameters.AddWithValue("@OldHomeEconomics", oldresult.HomeEconomics);
        } else {
            updateCommand.Parameters.AddWithValue("@OldHomeEconomics", DBNull.Value); }
        if (oldresult.MoralInstruction != null) {
            updateCommand.Parameters.AddWithValue("@OldMoralInstruction", oldresult.MoralInstruction);
        } else {
            updateCommand.Parameters.AddWithValue("@OldMoralInstruction", DBNull.Value); }
        if (oldresult.TotalScore.HasValue == true) {
            updateCommand.Parameters.AddWithValue("@OldTotalScore", oldresult.TotalScore);
        } else {
            updateCommand.Parameters.AddWithValue("@OldTotalScore", DBNull.Value); }
        if (oldresult.Grade != null) {
            updateCommand.Parameters.AddWithValue("@OldGrade", oldresult.Grade);
        } else {
            updateCommand.Parameters.AddWithValue("@OldGrade", DBNull.Value); }
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

    public static bool Delete(result clsresult)
    {
        SqlConnection connection = SchoolData.GetConnection();
        string deleteStatement
            = "DELETE FROM "  
            + "     [result] "
            + "WHERE " 
            + "     [ResultId] = @OldResultId " 
            + " AND ((@OldExamId IS NULL AND [ExamId] IS NULL) OR [ExamId] = @OldExamId) " 
            + " AND ((@OldStudentId IS NULL AND [StudentId] IS NULL) OR [StudentId] = @OldStudentId) " 
            + " AND ((@OldMathematics IS NULL AND [Mathematics] IS NULL) OR [Mathematics] = @OldMathematics) " 
            + " AND ((@OldEnglishLanguage IS NULL AND [EnglishLanguage] IS NULL) OR [EnglishLanguage] = @OldEnglishLanguage) " 
            + " AND ((@OldQuantitativeReasoning IS NULL AND [QuantitativeReasoning] IS NULL) OR [QuantitativeReasoning] = @OldQuantitativeReasoning) " 
            + " AND ((@OldElementaryScience IS NULL AND [ElementaryScience] IS NULL) OR [ElementaryScience] = @OldElementaryScience) " 
            + " AND ((@OldSocialStudies IS NULL AND [SocialStudies] IS NULL) OR [SocialStudies] = @OldSocialStudies) " 
            + " AND ((@OldVocationalAptitude IS NULL AND [VocationalAptitude] IS NULL) OR [VocationalAptitude] = @OldVocationalAptitude) " 
            + " AND ((@OldHealthEducation IS NULL AND [HealthEducation] IS NULL) OR [HealthEducation] = @OldHealthEducation) " 
            + " AND ((@OldComputer IS NULL AND [Computer] IS NULL) OR [Computer] = @OldComputer) " 
            + " AND ((@OldHomeEconomics IS NULL AND [HomeEconomics] IS NULL) OR [HomeEconomics] = @OldHomeEconomics) " 
            + " AND ((@OldMoralInstruction IS NULL AND [MoralInstruction] IS NULL) OR [MoralInstruction] = @OldMoralInstruction) " 
            + " AND ((@OldTotalScore IS NULL AND [TotalScore] IS NULL) OR [TotalScore] = @OldTotalScore) " 
            + " AND ((@OldGrade IS NULL AND [Grade] IS NULL) OR [Grade] = @OldGrade) " 
            + "";
        SqlCommand deleteCommand = new SqlCommand(deleteStatement, connection);
        deleteCommand.CommandType = CommandType.Text;
        deleteCommand.Parameters.AddWithValue("@OldResultId", clsresult.ResultId);
        if (clsresult.ExamId.HasValue == true) {
            deleteCommand.Parameters.AddWithValue("@OldExamId", clsresult.ExamId);
        } else {
            deleteCommand.Parameters.AddWithValue("@OldExamId", DBNull.Value); }
        if (clsresult.StudentId.HasValue == true) {
            deleteCommand.Parameters.AddWithValue("@OldStudentId", clsresult.StudentId);
        } else {
            deleteCommand.Parameters.AddWithValue("@OldStudentId", DBNull.Value); }
        if (clsresult.Mathematics != null) {
            deleteCommand.Parameters.AddWithValue("@OldMathematics", clsresult.Mathematics);
        } else {
            deleteCommand.Parameters.AddWithValue("@OldMathematics", DBNull.Value); }
        if (clsresult.EnglishLanguage != null) {
            deleteCommand.Parameters.AddWithValue("@OldEnglishLanguage", clsresult.EnglishLanguage);
        } else {
            deleteCommand.Parameters.AddWithValue("@OldEnglishLanguage", DBNull.Value); }
        if (clsresult.QuantitativeReasoning != null) {
            deleteCommand.Parameters.AddWithValue("@OldQuantitativeReasoning", clsresult.QuantitativeReasoning);
        } else {
            deleteCommand.Parameters.AddWithValue("@OldQuantitativeReasoning", DBNull.Value); }
        if (clsresult.ElementaryScience != null) {
            deleteCommand.Parameters.AddWithValue("@OldElementaryScience", clsresult.ElementaryScience);
        } else {
            deleteCommand.Parameters.AddWithValue("@OldElementaryScience", DBNull.Value); }
        if (clsresult.SocialStudies != null) {
            deleteCommand.Parameters.AddWithValue("@OldSocialStudies", clsresult.SocialStudies);
        } else {
            deleteCommand.Parameters.AddWithValue("@OldSocialStudies", DBNull.Value); }
        if (clsresult.VocationalAptitude != null) {
            deleteCommand.Parameters.AddWithValue("@OldVocationalAptitude", clsresult.VocationalAptitude);
        } else {
            deleteCommand.Parameters.AddWithValue("@OldVocationalAptitude", DBNull.Value); }
        if (clsresult.HealthEducation != null) {
            deleteCommand.Parameters.AddWithValue("@OldHealthEducation", clsresult.HealthEducation);
        } else {
            deleteCommand.Parameters.AddWithValue("@OldHealthEducation", DBNull.Value); }
        if (clsresult.Computer != null) {
            deleteCommand.Parameters.AddWithValue("@OldComputer", clsresult.Computer);
        } else {
            deleteCommand.Parameters.AddWithValue("@OldComputer", DBNull.Value); }
        if (clsresult.HomeEconomics != null) {
            deleteCommand.Parameters.AddWithValue("@OldHomeEconomics", clsresult.HomeEconomics);
        } else {
            deleteCommand.Parameters.AddWithValue("@OldHomeEconomics", DBNull.Value); }
        if (clsresult.MoralInstruction != null) {
            deleteCommand.Parameters.AddWithValue("@OldMoralInstruction", clsresult.MoralInstruction);
        } else {
            deleteCommand.Parameters.AddWithValue("@OldMoralInstruction", DBNull.Value); }
        if (clsresult.TotalScore.HasValue == true) {
            deleteCommand.Parameters.AddWithValue("@OldTotalScore", clsresult.TotalScore);
        } else {
            deleteCommand.Parameters.AddWithValue("@OldTotalScore", DBNull.Value); }
        if (clsresult.Grade != null) {
            deleteCommand.Parameters.AddWithValue("@OldGrade", clsresult.Grade);
        } else {
            deleteCommand.Parameters.AddWithValue("@OldGrade", DBNull.Value); }
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

 
