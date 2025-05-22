using Domain.Common;

namespace Domain.Enrollment;
public sealed class EnrollmentStatus : Enumeration<EnrollmentStatus>
{
    public static readonly EnrollmentStatus Pending = new(1, "Pending");
    public static readonly EnrollmentStatus Approved = new(2, "Approved");
    public static readonly EnrollmentStatus Rejected = new(3, "Rejected");
    public static readonly EnrollmentStatus Completed = new(4, "Completed");

    private EnrollmentStatus(int Id, string name) : base(Id, name) { }
}

