using System;
public class teacher
{
    private Int32 m_TeacherId;
    private Nullable<Int32> m_ClassId;
    private String m_FullName;
    private Nullable<DateTime> m_DateOfJoin;
    private String m_HomeAddress;
    private String m_PhoneNumber;

    public teacher() { }

    public Int32 TeacherId
    {
        get
        {
            return m_TeacherId;
        }
        set
        {
            m_TeacherId = value;
        }
    }
    public Nullable<Int32> ClassId
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

    public String FullName
    {
        get
        {
            return m_FullName;
        }
        set
        {
            m_FullName = value;
        }
    }

    public Nullable<DateTime> DateOfJoin
    {
        get
        {
            return m_DateOfJoin;
        }
        set
        {
            m_DateOfJoin = value;
        }
    }

    public String HomeAddress
    {
        get
        {
            return m_HomeAddress;
        }
        set
        {
            m_HomeAddress = value;
        }
    }

    public String PhoneNumber
    {
        get
        {
            return m_PhoneNumber;
        }
        set
        {
            m_PhoneNumber = value;
        }
    }

}

 
