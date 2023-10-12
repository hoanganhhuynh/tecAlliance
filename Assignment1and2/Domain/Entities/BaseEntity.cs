using System;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
	public class BaseEntity
	{
        [JsonInclude]
        public Guid Id { get; private set;}
		public BaseEntity(Guid id)
		{
			Id = id;
		}
	}
}

