using System;
public class studentclass
{
    private Int32 m_ClassId;
    private String m_ClassName;

    public studentclass() { }

    public Int32 ClassId
    {
        get
        {
            return m_ClassId;
        }
        set
        {
            m_ClassId = value;
        }
    }
    public String ClassName
    {
        get
        {
            return m_ClassName;
        }
        set
        {
            m_ClassName = value;
        }
    }

}

 
