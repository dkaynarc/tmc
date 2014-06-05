#region Header

/// FileName: ICalibrateable.cs
/// Author: Denis Kaynarca (denis@dkaynarca.com)

#endregion Header


namespace Tmc.Common
{
    public interface ICalibrateable
    {
        ICalibrationData Calibrate();

        void SetCalibrationData(ICalibrationData data);

        void Register();

        void Unregister();
    }
}