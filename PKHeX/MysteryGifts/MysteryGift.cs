﻿using System;
using System.Linq;

namespace PKHeX
{
    public abstract class MysteryGift
    {

        /// <summary>
        /// Determines whether or not the given length of bytes is valid for a mystery gift.
        /// </summary>
        /// <param name="len">Length, in bytes, of the data of which to determine validity.</param>
        /// <returns>A boolean indicating whether or not the given length is valid for a mystery gift.</returns>
        public static bool getIsMysteryGift(long len)
        {
            return new[] { WC6.SizeFull, WC6.Size, PGF.Size, PGT.Size, PCD.Size }.Contains((int)len);
        }

        /// <summary>
        /// Converts the given data to a <see cref="MysteryGift"/>.
        /// </summary>
        /// <param name="data">Raw data of the mystery gift.</param>
        /// <param name="ext">Extension of the file from which the <paramref name="data"/> was retrieved.</param>
        /// <returns>An instance of <see cref="MysteryGift"/> representing the given data, or null if <paramref name="data"/> or <paramref name="ext"/> is invalid.</returns>
        /// <remarks>This overload differs from <see cref="getMysteryGift(byte[])"/> by checking the <paramref name="data"/>/<paramref name="ext"/> combo for validity.  If either is invalid, a null reference is returned.</remarks>
        public static MysteryGift getMysteryGift(byte[] data, string ext)
        {
            if (data.Length == WC6.SizeFull && ext == ".wc6full")
                return new WC6(data);
            if (data.Length == WC6.Size && ext == ".wc6")
                return new WC6(data);
            if (data.Length == PGF.Size && ext == ".pgf")
                return new PGF(data);
            if (data.Length == PGT.Size && ext == ".pgt")
                return new PGT(data);
            if (data.Length == PCD.Size && ext == ".pcd")
                return new PCD(data);
            return null;
        }

        /// <summary>
        /// Converts the given data to a <see cref="MysteryGift"/>.
        /// </summary>
        /// <param name="data">Raw data of the mystery gift.</param>
        /// <returns>An instance of <see cref="MysteryGift"/> representing the given data, or null if <paramref name="data"/> is invalid.</returns>
        public static MysteryGift getMysteryGift(byte[] data)
        {
            switch (data.Length)
            {
                case WC6.SizeFull:
                case WC6.Size:
                    return new WC6(data);
                case PGF.Size:
                    return new PGF(data);
                case PGT.Size:
                    return new PGT(data);
                case PCD.Size:
                    return new PCD(data);
                default:
                    return null;
            }
        }

        public string Extension => "." + GetType().Name.ToLower();
        public string FileName => getCardHeader() + "." + Extension;
        public virtual byte[] Data { get; set; }
        public abstract PKM convertToPKM(SaveFile SAV);
        public abstract int Format { get; }

        public MysteryGift Clone()
        {
            byte[] data = (byte[])Data.Clone();
            return getMysteryGift(data);
        }
        public string Type => GetType().Name;

        // Properties
        public abstract bool GiftUsed { get; set; }
        public abstract string CardTitle { get; set; }
        public abstract int CardID { get; set; }

        public abstract bool IsItem { get; set; }
        public abstract int Item { get; set; }

        public abstract bool IsPokémon { get; set; }
        public virtual int Quantity { get { return 1; } set { } }
        public bool Empty => Data.SequenceEqual(new byte[Data.Length]);

        public string getCardHeader() => (CardID > 0 ? $"Card #: {CardID.ToString("0000")}" : "N/A") + $" - {CardTitle.Replace('\u3000',' ').Trim()}";

        // Search Properties
        public virtual int Species { get { return -1; } set { } }
        public virtual int[] Moves => new int[0];
        public virtual bool IsShiny => false;
        public virtual bool IsEgg { get { return false; } set { } }
        public virtual int HeldItem { get { return -1; } set { } }
        public bool Gen7 => Format == 7;
        public bool Gen6 => Format == 6;
        public bool Gen5 => Format == 5;
        public bool Gen4 => Format == 4;
        public bool Gen3 => Format == 3;
    }
}
