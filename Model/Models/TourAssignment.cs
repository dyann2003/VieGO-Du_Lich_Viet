using System;
using System.Collections.Generic;

namespace Model.Models;

public partial class TourAssignment
{
    public int AssignmentId { get; set; }

    public int TourId { get; set; }

    public int ScheduleId { get; set; }

    public int GuideId { get; set; }

    public DateOnly AssignmentDate { get; set; }

    public string Status { get; set; } = null!;

    public virtual TourGuide Guide { get; set; } = null!;

    public virtual TourSchedule Schedule { get; set; } = null!;

    public virtual Tour Tour { get; set; } = null!;
}
