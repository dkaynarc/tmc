#region Header

/// FileName: ICalibrationData.cs
/// Author: Denis Kaynarca (denis@dkaynarca.com)

#endregion Header

using System;

namespace Tmc.Common
{
    public interface ICalibrationData
    {
        Type ParentType { get; set; }
    }
}