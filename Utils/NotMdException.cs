using System;

namespace TalkToStudentsBack.Utils
{
    public class NotMdException: Exception
    {
        public NotMdException() : base("The result file does not have .md extension")
        {

        }
    }
}