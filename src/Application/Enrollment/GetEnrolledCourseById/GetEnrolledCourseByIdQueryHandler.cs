using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.DTO.Enrollment;
using Domain.Enrollment;
using SharedKernel;

namespace Application.Enrollment.GetEnrolledCourseById;
internal sealed class GetEnrolledCourseByIdQueryHandler(IUnitOfWork unitOfWork) : IQueryHandler<GetEnrolledCourseByIdQuery, EnrollmentSuccessResponseDto>
{
    public async Task<Result<EnrollmentSuccessResponseDto>> Handle(GetEnrolledCourseByIdQuery request, CancellationToken cancellationToken)
    {
        Domain.Enrollment.Enrollment? enrollCred = await unitOfWork.EnrollmentRepository.GetAsync(e => e.Id == request.Id, cancellationToken: cancellationToken);

        if (enrollCred == null)
        {
            return Result.Failure<EnrollmentSuccessResponseDto>(EnrollmentError.NotFound(request.Id));
        }

        var response = (EnrollmentSuccessResponseDto)enrollCred!;

        return Result.Success(response);
    }
}
