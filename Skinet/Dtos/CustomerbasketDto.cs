using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Skinet.Dtos
{
    public class CustomerbasketDto
    {
        public CustomerbasketDto()
        {
                
        }
        public CustomerbasketDto(string id)
        {
            Id = id;
        }
        public string Id { get; set; }
        public List<BasketItemDto> Items { get; set; } 
    }
}
