namespace FlightsApp;

/// <summary>Рейс літака: міста та дати відправлення і прибуття.</summary>
public class Airplane
{
    private string departureCity;
    private string arrivalCity;
    private Date departureDate;
    private Date arrivalDate;

    /// <summary>Конструктор за замовчуванням: невідомий маршрут, відправлення і прибуття - зараз.</summary>
    public Airplane()
        : this("Невідомо", "Невідомо")
    {
    }

    /// <summary>Маршрут без розкладу: відправлення і прибуття - поточний момент.</summary>
    public Airplane(string departureCity, string arrivalCity)
        : this(departureCity, arrivalCity, new Date(), new Date())
    {
    }

    public Airplane(string departureCity, string arrivalCity, Date departureDate, Date arrivalDate)
    {
        this.departureCity = ValidateCity(departureCity);
        this.arrivalCity = ValidateCity(arrivalCity);
        this.departureDate = departureDate ?? throw new ArgumentException("дата відправлення не задана");
        this.arrivalDate = arrivalDate ?? throw new ArgumentException("дата прибуття не задана");

        if (arrivalDate.ToDateTime() < departureDate.ToDateTime())
            throw new ArgumentException("прибуття не може бути раніше за відправлення");
    }

    /// <summary>Конструктор копіювання (глибоке копіювання: дати теж копіюються).</summary>
    public Airplane(Airplane other)
        : this(other.departureCity, other.arrivalCity, new Date(other.departureDate), new Date(other.arrivalDate))
    {
    }

    public string DepartureCity
    {
        get => departureCity;
        set => departureCity = ValidateCity(value);
    }

    public string ArrivalCity
    {
        get => arrivalCity;
        set => arrivalCity = ValidateCity(value);
    }

    public Date DepartureDate
    {
        get => departureDate;
        set => departureDate = value ?? throw new ArgumentException("дата відправлення не задана");
    }

    public Date ArrivalDate
    {
        get => arrivalDate;
        set => arrivalDate = value ?? throw new ArgumentException("дата прибуття не задана");
    }

    /// <summary>Сумарний час подорожі у хвилинах.</summary>
    public int GetTotalTime() => (int)(arrivalDate.ToDateTime() - departureDate.ToDateTime()).TotalMinutes;

    /// <summary>true, якщо відправлення і прибуття відбуваються в той самий день.</summary>
    public bool IsArrivingToday() => departureDate.IsSameDay(arrivalDate);

    public override string ToString() =>
        $"{departureCity} -> {arrivalCity}, {departureDate} - {arrivalDate}";

    private static string ValidateCity(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("назва міста не може бути порожньою");
        return value.Trim();
    }
}
