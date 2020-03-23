using System.Diagnostics.Tracing;

namespace App
{
    public class Tags
    {
        public const EventTags MSSQL = (EventTags)1;
        public const EventTags Oracle = (EventTags)2;
        public const EventTags Db2 = (EventTags)3;
    }
}