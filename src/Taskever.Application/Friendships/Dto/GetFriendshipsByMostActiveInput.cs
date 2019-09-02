using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace Taskever.Friendships.Dto
{
    public class GetFriendshipsByMostActiveInput : ILimitedResultRequest
    {
        private const int MaxMaxResultCount = 100;
        
        [Range(1, MaxMaxResultCount)]
        public int MaxResultCount { get; set; }

        public GetFriendshipsByMostActiveInput()
        {
            MaxResultCount = MaxMaxResultCount;
        }
    }
}