using System;
using System.Collections.Generic;
using System.Text;

namespace WMS.Application.DTOs;

public class AuditLogDto
{
    public string EntityName { get; set; } = string.Empty;

    public int RecordId { get; set; }

    public string Action { get; set; } = string.Empty;

    public int CreatedBy { get; set; }
}