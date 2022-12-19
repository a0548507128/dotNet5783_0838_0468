

namespace DO;

public class EntityNotFound:Exception
{ 
    public string RequestedItemNotFound { get; set; }

    public EntityNotFound(string msg) : base(msg)
    {
    }
}
public class DuplicateID:Exception
{
    public string ItemAlreadyExists { get; set; }

    public DuplicateID(string msg) : base(msg)
    {
    }
}
[Serializable]
public class DalConfigException : Exception
{
    public DalConfigException(string msg) : base(msg) { }
    public DalConfigException(string msg, Exception ex) : base(msg, ex) { }
}

