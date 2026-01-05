using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4_Observer
{
    public class PatientVitalSigns
    {
        public int HeartRate { get; set; }
        public string BloodPressure { get; set; }
        public double Temperature { get; set; }
        public int OxygenLevel { get; set; }
        public DateTime RecordedAt { get; set; }
    }

    public interface IPatientObserver
    {
        void Attach(IObserver observer);
        void Detach(IObserver observer);
        void Notify();
    }

    public interface IObserver
    {
        void Update(PatientVitalSigns vitalSigns);
    }

    public class Patient : IPatientObserver
    {
        private List<IObserver> _observers = new List<IObserver>();
        private PatientVitalSigns _vitalSigns;

        public string PatientId { get; set; }
        public string PatientName { get; set; }

        public PatientVitalSigns VitalSigns
        {
            get => _vitalSigns;
            set
            {
                _vitalSigns = value;
                Console.WriteLine($"\nPatient Monitor Alert for {PatientName} (ID: {PatientId})");
                Console.WriteLine($"   Heart Rate: {value.HeartRate} bpm");
                Console.WriteLine($"   Blood Pressure: {value.BloodPressure}");
                Console.WriteLine($"   Temperature: {value.Temperature}°C");
                Console.WriteLine($"   Oxygen Level: {value.OxygenLevel}%");
                Notify();
            }
        }

        public void Attach(IObserver observer)
        {
            _observers.Add(observer);
            Console.WriteLine($" Observer attached: {observer.GetType().Name}");
        }

        public void Detach(IObserver observer)
        {
            _observers.Remove(observer);
            Console.WriteLine($" Observer detached: {observer.GetType().Name}");
        }

        public void Notify()
        {
            foreach (var observer in _observers)
            {
                observer.Update(_vitalSigns);
            }
        }
    }

    public class Doctor : IObserver
    {
        private string _doctorName;

        public Doctor(string doctorName)
        {
            _doctorName = doctorName;
        }

        public void Update(PatientVitalSigns vitalSigns)
        {
            if (vitalSigns.HeartRate > 100 || vitalSigns.HeartRate < 60)
            {
                Console.WriteLine($"Dr. {_doctorName}: ABNORMAL HEART RATE DETECTED!");
            }

            if (vitalSigns.OxygenLevel < 95)
            {
                Console.WriteLine($"Dr. {_doctorName}: LOW OXYGEN LEVEL - IMMEDIATE ATTENTION REQUIRED!");
            }
        }
    }

    public class FamilyMember : IObserver
    {
        private string _familyContactName;
        private string _familyPhone;

        public FamilyMember(string contactName, string phone)
        {
            _familyContactName = contactName;
            _familyPhone = phone;
        }

        public void Update(PatientVitalSigns vitalSigns)
        {
            if (vitalSigns.HeartRate > 110 || vitalSigns.OxygenLevel < 92)
            {
                Console.WriteLine($"Sending SMS to {_familyContactName} ({_familyPhone}): Patient condition requires attention");
            }
        }
    }
}
