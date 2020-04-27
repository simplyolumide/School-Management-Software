using System;
public class students
{
    private Int32 m_StudentId;
    private Nullable<Int32> m_ClassId;
    private String m_FullName;
    private Nullable<DateTime> m_DateOfBirth;
    private String m_HomeAddress;
    private String m_Gender;
    private String m_Father;
    private String m_Mother;
    private String m_ParentContact;

    public students() { }

    public Int32 StudentId
    {
        get
        {
            return m_StudentId;
        }
        set
        {
            m_StudentId = value;
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

    public Nullable<DateTime> DateOfBirth
    {
        get
        {
            return m_DateOfBirth;
        }
        set
        {
            m_DateOfBirth = value;
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

    public String Gender
    {
        get
        {
            return m_Gender;
        }
        set
        {
            m_Gender = value;
        }
    }
    public String Father
    {
        get
        {
            return m_Father;
        }
        set
        {
            m_Father = value;
        }
    }

    public String Mother
    {
        get
        {
            return m_Mother;
        }
        set
        {
            m_Mother = value;
        }
    }

    public String ParentContact
    {
        get
        {
            return m_ParentContact;
        }
        set
        {
            m_ParentContact = value;
        }
    }

}

 
