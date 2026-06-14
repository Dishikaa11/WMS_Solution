using System;
using System.Collections.Generic;
using System.Text;

namespace WMS.Application.DTOs;

public class ClientDto
{
    public int ClientId { get; set; }

    public string ClientName { get; set; } = string.Empty;

    public string? ClientAddress { get; set; }

    public string? ClientPhoneNumber { get; set; }

    public string? ClientLocation { get; set; }

    public bool Status { get; set; }
}