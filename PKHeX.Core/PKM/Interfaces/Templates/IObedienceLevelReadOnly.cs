
/// <summary>
/// Exposes information about the level the Pokémon was obtained.
/// </summary>
public interface IObedienceLevelReadOnly
{
    /// <summary>
    /// Indicates the level the Pokémon was obtained by the current handler.
    /// </summary>
    byte Obedience_Level { get; } // no setter, use for Encounters
}
