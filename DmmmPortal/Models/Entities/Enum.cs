using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace DmmmPortal.Models.Entities
{
  
 
    public enum MailStatus
    {
        Sent = 1,

        SavedAsDraft = 2,

        Archived = 3
    }

    public enum SessionStatus
    {
        [Description("Not Used")]
        NotUsed = 1,

        [Description("Used")]
        Used = 2,

        [Description("Current")]
        Current = 3
    }

    public enum SchoolType
    {
        [Description("Nursery & Primary")]
        NP = 1,

        [Description("Secondary")]
        Secondary = 2,

        [Description("Tertiary")]
        Tertiary = 3
    }

    public enum PostStatus
    {
        [Description("Published")]
        Published = 1,

        [Description("Deleted")]
        Deleted = 2
    }

    public enum WhoSeePost
    {
        [Description("All")]
        All = 0,
        [Description("Student")]
        Student = 1,

        [Description("Staff")]
        Staff = 2
    }

 
    public enum MessageStatus
    {
        [Description("NotReplied")]
        NotReplied = 1,

        [Description("Replied")]
        Replied = 2,
            [Description("Closed")]
        Closed = 3
    }

    public enum UserStatus
    {
        [Description("NONE")]
        NONE = 0,
        [Description("Active")]
        Active = 1,
        [Description("On Futher Studies")]
        OnFutherStudies = 2,
        [Description("On Mission")]
        OnMission = 3,
        [Description("Departed")]
        Departed = 4,



    }
}