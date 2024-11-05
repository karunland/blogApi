using System.ComponentModel;

namespace BlogApi.Core.Enums;

public enum BlogStatusEnum
{
    [Description("Draft")]
    Draft,
    [Description("Published")]
    Published,
    [Description("Archived")]
    Archived
}