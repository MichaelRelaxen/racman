namespace racman.offsets
{
    public interface IReadMemory
    {

        /// <summary>
        /// Reads data from the memory of the game.
        /// </summary>
        /// <param name="address"> The address to read from </param>
        /// <param name="size"> The size of the data to read </param>
        /// <returns> The byte array of the data read </returns>
        byte[] ReadMemory(uint address, uint size);
    }
}
