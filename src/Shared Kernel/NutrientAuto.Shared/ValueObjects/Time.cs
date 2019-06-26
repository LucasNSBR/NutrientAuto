using System;

namespace NutrientAuto.Shared.ValueObjects
{
    public class Time : ValueObject<Time>, IComparable<Time>
    {
        public int Hour { get; private set; }
        public int Minute { get; private set; }
        public int Second { get; private set; }

        protected Time()
        {
        }

        public Time(int hour, int minute, int second)
        {
            Hour = hour;
            Minute = minute;
            Second = second;

            if (hour < 0 && hour > 23)
                throw new ArgumentException("Hora inválida. A hora deve estar no formato de 24 horas.");
            if (minute < 0 && minute > 59)
                throw new ArgumentException("Minuto inválido. O minuto deve estar no raio 0 e 59.");
            if (second < 0 && second > 59)
                throw new ArgumentException("Segundo inválido. O segundo deve estar no raio 0 e 59.");
        }

        public static Time FromDateTime(DateTime dateTime)
        {
            return new Time(dateTime.Hour, dateTime.Minute, dateTime.Second);
        }

        public DateTime ToDateTime()
        {
            return new DateTime(0, 0, 0, Hour, Minute, Second);
        }

        public int ToMinutes()
        {
            int hours = Hour;
            int minutes = Minute;
            int countedMinutes = 0;

            while(hours > 0)
            {
                countedMinutes = countedMinutes + 60;
                hours--;
            }

            countedMinutes = countedMinutes + minutes;
            return countedMinutes;
        }

        public int CompareTo(Time other)
        {
            int hourComparation = Hour.CompareTo(other.Hour);

            if (hourComparation == 0)
            {
                int minuteComparation = Minute.CompareTo(other.Minute);
                if (minuteComparation == 0)
                    return Second.CompareTo(other.Second);

                return minuteComparation;
            }

            return hourComparation;
        }

        public override string ToString()
        {
            return $"{Hour} horas, {Minute} minutos e {Second} segundos";
        }
    }
}
