// Quran Juz data structure
// Each Juz contains information about the starting and ending verses

export const juzData = [
  {
    number: 1,
    name: "Juz 1",
    startSurah: 1,
    startSurahName: "Al-Fatihah",
    startVerse: 1,
    endSurah: 2,
    endSurahName: "Al-Baqarah",
    endVerse: 141,
    description: "Starts with Al-Fatihah and continues through Al-Baqarah"
  },
  {
    number: 2,
    name: "Juz 2",
    startSurah: 2,
    startSurahName: "Al-Baqarah",
    startVerse: 142,
    endSurah: 2,
    endSurahName: "Al-Baqarah",
    endVerse: 252,
    description: "Continues Al-Baqarah"
  },
  {
    number: 3,
    name: "Juz 3",
    startSurah: 2,
    startSurahName: "Al-Baqarah",
    startVerse: 253,
    endSurah: 3,
    endSurahName: "Ali 'Imran",
    endVerse: 92,
    description: "Ends Al-Baqarah and continues through Ali 'Imran"
  },
  {
    number: 4,
    name: "Juz 4",
    startSurah: 3,
    startSurahName: "Ali 'Imran",
    startVerse: 93,
    endSurah: 4,
    endSurahName: "An-Nisa",
    endVerse: 23,
    description: "Continues Ali 'Imran and starts An-Nisa"
  },
  {
    number: 5,
    name: "Juz 5",
    startSurah: 4,
    startSurahName: "An-Nisa",
    startVerse: 24,
    endSurah: 4,
    endSurahName: "An-Nisa",
    endVerse: 147,
    description: "Continues An-Nisa"
  },
  {
    number: 6,
    name: "Juz 6",
    startSurah: 4,
    startSurahName: "An-Nisa",
    startVerse: 148,
    endSurah: 5,
    endSurahName: "Al-Ma'idah",
    endVerse: 81,
    description: "Ends An-Nisa and continues through Al-Ma'idah"
  },
  {
    number: 7,
    name: "Juz 7",
    startSurah: 5,
    startSurahName: "Al-Ma'idah",
    startVerse: 82,
    endSurah: 6,
    endSurahName: "Al-An'am",
    endVerse: 110,
    description: "Ends Al-Ma'idah and continues through Al-An'am"
  },
  {
    number: 8,
    name: "Juz 8",
    startSurah: 6,
    startSurahName: "Al-An'am",
    startVerse: 111,
    endSurah: 7,
    endSurahName: "Al-A'raf",
    endVerse: 87,
    description: "Ends Al-An'am and continues through Al-A'raf"
  },
  {
    number: 9,
    name: "Juz 9",
    startSurah: 7,
    startSurahName: "Al-A'raf",
    startVerse: 88,
    endSurah: 8,
    endSurahName: "Al-Anfal",
    endVerse: 40,
    description: "Ends Al-A'raf and continues through Al-Anfal"
  },
  {
    number: 10,
    name: "Juz 10",
    startSurah: 8,
    startSurahName: "Al-Anfal",
    startVerse: 41,
    endSurah: 9,
    endSurahName: "At-Tawbah",
    endVerse: 92,
    description: "Ends Al-Anfal and continues through At-Tawbah"
  },
  {
    number: 11,
    name: "Juz 11",
    startSurah: 9,
    startSurahName: "At-Tawbah",
    startVerse: 93,
    endSurah: 11,
    endSurahName: "Hud",
    endVerse: 5,
    description: "Ends At-Tawbah, includes Yunus, and starts Hud"
  },
  {
    number: 12,
    name: "Juz 12",
    startSurah: 11,
    startSurahName: "Hud",
    startVerse: 6,
    endSurah: 12,
    endSurahName: "Yusuf",
    endVerse: 52,
    description: "Ends Hud and continues through Yusuf"
  },
  {
    number: 13,
    name: "Juz 13",
    startSurah: 12,
    startSurahName: "Yusuf",
    startVerse: 53,
    endSurah: 14,
    endSurahName: "Ibrahim",
    endVerse: 52,
    description: "Ends Yusuf, includes Ar-Ra'd, and completes Ibrahim"
  },
  {
    number: 14,
    name: "Juz 14",
    startSurah: 15,
    startSurahName: "Al-Hijr",
    startVerse: 1,
    endSurah: 16,
    endSurahName: "An-Nahl",
    endVerse: 128,
    description: "Includes Al-Hijr and completes An-Nahl"
  },
  {
    number: 15,
    name: "Juz 15",
    startSurah: 17,
    startSurahName: "Al-Isra",
    startVerse: 1,
    endSurah: 18,
    endSurahName: "Al-Kahf",
    endVerse: 74,
    description: "Includes Al-Isra and continues through Al-Kahf"
  },
  {
    number: 16,
    name: "Juz 16",
    startSurah: 18,
    startSurahName: "Al-Kahf",
    startVerse: 75,
    endSurah: 20,
    endSurahName: "Ta-Ha",
    endVerse: 135,
    description: "Ends Al-Kahf, includes Maryam, and continues through Ta-Ha"
  },
  {
    number: 17,
    name: "Juz 17",
    startSurah: 21,
    startSurahName: "Al-Anbya",
    startVerse: 1,
    endSurah: 22,
    endSurahName: "Al-Hajj",
    endVerse: 78,
    description: "Includes Al-Anbya and completes Al-Hajj"
  },
  {
    number: 18,
    name: "Juz 18",
    startSurah: 23,
    startSurahName: "Al-Mu'minun",
    startVerse: 1,
    endSurah: 25,
    endSurahName: "Al-Furqan",
    endVerse: 20,
    description: "Includes Al-Mu'minun, An-Nur, and starts Al-Furqan"
  },
  {
    number: 19,
    name: "Juz 19",
    startSurah: 25,
    startSurahName: "Al-Furqan",
    startVerse: 21,
    endSurah: 27,
    endSurahName: "An-Naml",
    endVerse: 55,
    description: "Ends Al-Furqan, includes Ash-Shu'ara, and continues through An-Naml"
  },
  {
    number: 20,
    name: "Juz 20",
    startSurah: 27,
    startSurahName: "An-Naml",
    startVerse: 56,
    endSurah: 29,
    endSurahName: "Al-'Ankabut",
    endVerse: 45,
    description: "Ends An-Naml, includes Al-Qasas, and continues through Al-'Ankabut"
  },
  {
    number: 21,
    name: "Juz 21",
    startSurah: 29,
    startSurahName: "Al-'Ankabut",
    startVerse: 46,
    endSurah: 33,
    endSurahName: "Al-Ahzab",
    endVerse: 30,
    description: "Ends Al-'Ankabut, includes Ar-Rum, Luqman, As-Sajdah, and continues through Al-Ahzab"
  },
  {
    number: 22,
    name: "Juz 22",
    startSurah: 33,
    startSurahName: "Al-Ahzab",
    startVerse: 31,
    endSurah: 36,
    endSurahName: "Ya-Sin",
    endVerse: 27,
    description: "Ends Al-Ahzab, includes Saba, Fatir, and starts Ya-Sin"
  },
  {
    number: 23,
    name: "Juz 23",
    startSurah: 36,
    startSurahName: "Ya-Sin",
    startVerse: 28,
    endSurah: 39,
    endSurahName: "Az-Zumar",
    endVerse: 31,
    description: "Ends Ya-Sin, includes As-Saffat, Sad, and starts Az-Zumar"
  },
  {
    number: 24,
    name: "Juz 24",
    startSurah: 39,
    startSurahName: "Az-Zumar",
    startVerse: 32,
    endSurah: 41,
    endSurahName: "Fussilat",
    endVerse: 46,
    description: "Ends Az-Zumar, includes Ghafir, and continues through Fussilat"
  },
  {
    number: 25,
    name: "Juz 25",
    startSurah: 41,
    startSurahName: "Fussilat",
    startVerse: 47,
    endSurah: 45,
    endSurahName: "Al-Jathiyah",
    endVerse: 37,
    description: "Ends Fussilat, includes Ash-Shuraa, Az-Zukhruf, Ad-Dukhan, and completes Al-Jathiyah"
  },
  {
    number: 26,
    name: "Juz 26",
    startSurah: 46,
    startSurahName: "Al-Ahqaf",
    startVerse: 1,
    endSurah: 51,
    endSurahName: "Adh-Dhariyat",
    endVerse: 30,
    description: "Includes Al-Ahqaf, Muhammad, Al-Fath, Al-Hujurat, Qaf, and starts Adh-Dhariyat"
  },
  {
    number: 27,
    name: "Juz 27",
    startSurah: 51,
    startSurahName: "Adh-Dhariyat",
    startVerse: 31,
    endSurah: 57,
    endSurahName: "Al-Hadid",
    endVerse: 29,
    description: "Ends Adh-Dhariyat, includes At-Tur, An-Najm, Al-Qamar, Ar-Rahman, Al-Waqi'ah, and completes Al-Hadid"
  },
  {
    number: 28,
    name: "Juz 28",
    startSurah: 58,
    startSurahName: "Al-Mujadila",
    startVerse: 1,
    endSurah: 66,
    endSurahName: "At-Tahrim",
    endVerse: 12,
    description: "Includes Al-Mujadila, Al-Hashr, Al-Mumtahanah, As-Saff, Al-Jumu'ah, Al-Munafiqun, At-Taghabun, At-Talaq, and completes At-Tahrim"
  },
  {
    number: 29,
    name: "Juz 29",
    startSurah: 67,
    startSurahName: "Al-Mulk",
    startVerse: 1,
    endSurah: 77,
    endSurahName: "Al-Mursalat",
    endVerse: 50,
    description: "Includes surahs from Al-Mulk to Al-Mursalat"
  },
  {
    number: 30,
    name: "Juz 30",
    startSurah: 78,
    startSurahName: "An-Naba",
    startVerse: 1,
    endSurah: 114,
    endSurahName: "An-Nas",
    endVerse: 6,
    description: "Includes all remaining surahs from An-Naba to An-Nas"
  }
];

// Function to get a specific juz by number
export function getJuz(juzNumber) {
  return juzData.find(juz => juz.number === juzNumber);
}

// Function to get all juz names for dropdown
export function getAllJuzNames() {
  return juzData.map(juz => ({
    value: juz.number,
    label: `${juz.name} (${juz.startSurahName} ${juz.startVerse} - ${juz.endSurahName} ${juz.endVerse})`
  }));
}
