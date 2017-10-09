using System;
using System.Collections.Generic;
using System.Text;

namespace FI.Library.Encoding
{
    public static class SearchUtility
    {
        private static int[] m_badCharacterShift;
        private static int[] m_goodSuffixShift;
        private static int[] m_suffixes;

        public static void DoPreProcessing(byte[] pattern)
        {
          /* Preprocessing */
          m_badCharacterShift = BuildBadCharacterShift(pattern);
          m_suffixes = FindSuffixes(pattern);
          m_goodSuffixShift = BuildGoodSuffixShift(pattern, m_suffixes);
        }
        /// <summary>
        /// Build the bad character shift array.
        /// </summary>
        /// <param name="pattern">Pattern for search</param>
        /// <returns>bad character shift array</returns>
        private static int[] BuildBadCharacterShift(byte[] pattern)
        {
            int[] badCharacterShift = new int[256];

            for (int c = 0; c < badCharacterShift.Length; ++c)
                badCharacterShift[c] = pattern.Length;
            for (int i = 0; i < pattern.Length - 1; ++i)
                badCharacterShift[pattern[i]] = pattern.Length - i - 1;

            return badCharacterShift;
        }
        /// <summary>
        /// Find suffixes in the pattern
        /// </summary>
        /// <param name="pattern">Pattern for search</param>
        /// <returns>Suffix array</returns>
        private static int[] FindSuffixes(byte[] pattern)
        {
            int f = 0, g;

            int patternLength = pattern.Length;
            int[] suffixes = new int[pattern.Length + 1];

            suffixes[patternLength - 1] = patternLength;
            g = patternLength - 1;
            for (int i = patternLength - 2; i >= 0; --i)
            {
                if (i > g && suffixes[i + patternLength - 1 - f] < i - g)
                    suffixes[i] = suffixes[i + patternLength - 1 - f];
                else
                {
                    if (i < g)
                        g = i;
                    f = i;
                    while (g >= 0 && (pattern[g] == pattern[g + patternLength - 1 - f]))
                        --g;
                    suffixes[i] = f - g;
                }
            }

            return suffixes;
        }
        /// <summary>
        /// Build the good suffix array.
        /// </summary>
        /// <param name="pattern">Pattern for search</param>
        /// <returns>Good suffix shift array</returns>
        private static int[] BuildGoodSuffixShift(byte[] pattern, int[] suff)
        {
            int patternLength = pattern.Length;
            int[] goodSuffixShift = new int[pattern.Length + 1];

            for (int i = 0; i < patternLength; ++i)
                goodSuffixShift[i] = patternLength;
            int j = 0;
            for (int i = patternLength - 1; i >= -1; --i)
                if (i == -1 || suff[i] == i + 1)
                    for (; j < patternLength - 1 - i; ++j)
                        if (goodSuffixShift[j] == patternLength)
                            goodSuffixShift[j] = patternLength - 1 - i;
            for (int i = 0; i <= patternLength - 2; ++i)
                goodSuffixShift[patternLength - 1 - suff[i]] = patternLength - 1 - i;

            return goodSuffixShift;
        }

        /// <summary>
        /// Return all matches of the pattern in specified text using the Boyer-Moore algorithm
        /// </summary>
        /// <param name="byteSource">text to be searched</param>
        /// <param name="startingIndex">Index at which search begins</param>
        /// <returns>IEnumerable which returns the indexes of pattern matches</returns>
        static IEnumerable<int> BoyerMooreMatch(byte[] byteSource, byte[] bytePattern, int startingIndex)
        {
            DoPreProcessing(bytePattern);
            int patternLength = bytePattern.Length;
            int sourceLength = byteSource.Length;

            /* Searching */
            int index = startingIndex;
            while (index <= sourceLength - patternLength)
            {
                int unmatched;
                for (unmatched = patternLength - 1;
                  unmatched >= 0 && (bytePattern[unmatched] == byteSource[unmatched + index]);
                  --unmatched
                )
                    ; // empty

                if (unmatched < 0)
                {
                    yield return index;
                    index += m_goodSuffixShift[0];
                }
                else
                    index += Math.Max(m_goodSuffixShift[unmatched],
                      m_badCharacterShift[byteSource[unmatched + index]] - patternLength + 1 + unmatched);
            }
        }

        /// <summary>
        /// Return all matches of the pattern in specified text using the Boyer-Moore algorithm
        /// </summary>
        /// <param name="byteSource">text to be searched</param>
        /// <returns>IEnumerable which returns the indexes of pattern matches</returns>
        public static IEnumerable<int> ByteSearch(byte[] byteSource, byte[] bytePattern)
        {
            return BoyerMooreMatch(byteSource, bytePattern, 0);
        }
    }
}
