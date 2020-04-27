using System;
public class exam
{
    private Int32 m_ExamId;
    private String m_ExamType;

    public exam() { }

    public Int32 ExamId
    {
        get
        {
            return m_ExamId;
        }
        set
        {
            m_ExamId = value;
        }
    }
    public String ExamType
    {
        get
        {
            return m_ExamType;
        }
        set
        {
            m_ExamType = value;
        }
    }

}

 
