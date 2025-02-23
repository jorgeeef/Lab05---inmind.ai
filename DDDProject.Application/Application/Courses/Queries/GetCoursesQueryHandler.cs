using DDDProject.Domain.Repositories;
using Lab05.Domain.Models;
using MediatR;

namespace DDDProject.Application.Application.Courses.Queries;

    public class GetCoursesQueryHandler : IRequestHandler<GetCoursesQuery, List<Course>>
    {
        private readonly ICourseRepository _courseRepository;

        public GetCoursesQueryHandler(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<List<Course>> Handle(GetCoursesQuery request, CancellationToken cancellationToken)
        {
            return (await _courseRepository.GetAllAsync()).ToList();
        }
    }