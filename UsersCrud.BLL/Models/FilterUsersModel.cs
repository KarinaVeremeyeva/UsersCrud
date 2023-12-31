﻿using UsersCrud.BLL.Enums;

namespace UsersCrud.BLL.Models
{
    public class FilterUsersModel
    {
        public string? NamePart { get; set; }

        public int? MinAge { get; set; }

        public int? MaxAge { get; set; }

        public string? EmailPart { get; set; }

        public UserOrder? UserOrder { get; set; }

        public int Page { get; set; }

        public IEnumerable<Guid>? RoleIds { get; set; }
    }
}
