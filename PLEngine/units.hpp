#ifndef UNITS_HPP
#define UNITS_HPP

// Value formatting options for user attributes and table columns
//
// NOTE TF_EditUnits() needs updating when unit mode addded
//

enum eTFUnit
{
 UNIT_TIME,         // time HH:MM SS
 UNIT_VALUE,        // Plain value
 UNIT_MONEY,        // $x.xx
 UNIT_RATESEC,      // rate/s
 UNIT_RATEMIN,      // rate/m
 UNIT_RATEHOUR,     // rate/h
 UNIT_RATEDAY,      // rate/d
 UNIT_RATEWEEK,     // rate/w
 UNIT_RATEMONTH,    // rate/M
 UNIT_RATEYEAR,     // rate/y
 UNIT_ABSTIME,      // calendar HH:MM SS
 UNIT_LABEL,        // label list
 UNIT_MONEY_U,      // $x
 UNIT_ABSTIME2,     // calendar HHMM
 UNIT_VALUESPACED,  // value with commas
 UNIT_TIMEOFDAY,    // Time of day HHMM
 UNIT_TIMEAMPM,     // Time of day 12hr
 UNIT_HEX,          // hexadecimal
 UNIT_TIME2,        // Time HH:MM:SS
 UNIT_ABSTIME3,     // calendar HH:MM:SS
 UNIT_MINUTES,      // Time minutes
 UNIT_HOURS,        // Time hours
 UNIT_DAYS,         // Time days
 UNIT_WEEKS,        // Time weeks
 UNIT_VALUE_F1,     // fixed xxxx.x
 UNIT_VALUE_F2,     // fixed xxxx.xx
 UNIT_VALUE_CF1,    // fixed x,xxx.x
 UNIT_VALUE_CF2,    // fixed x,xxx.xx
 UNIT_DATEONLY,     // only date
 UNIT_DAYDATE,      // jan 3 feb, 2000
 UNIT_WEEKDAY1,     // week and day from 1
 UNIT_VALUE_RJZ,    // right justified zero filled 000XXX
 UNIT_ABSTIME3U,    // US date HH:MM:SS
 UNIT_LONG_EW,      // Longitude DD:MM:SS[E|W]
 UNIT_LONG_PRECISE, // Longitude [-]DD:MM:SS.SS
 UNIT_LAT_NS,       // Latitude DD:MM:SS[N|S]
 UNIT_LAT_PRECISE,  // Latitude [-]DD:MM:SS.SS
 UNIT_TIMEOFDAY_2,  // Time of day HH:MM
 UNIT_DATE2,        // Calendar YYYY-MM-DD
 UNIT_FREETEXT,     // (free text)
 UNIT_MONEY_R,      // Rand currency
 UNIT_MONEY_RU,     // Rand currency no cents
 UNIT_MONEY_E,      // Euro currency
 UNIT_MONEY_EU,     // Euro currency no cents
 UNIT_TIME3,        // Time HH:MM (no seconds)
 UNIT_ABSTIMEMS,    // Calendar HH:MM:SS.SSS
 UNIT_DATE2TIME,    // Calendar YYYY-MM-DD HH:MM:SS
 UNIT_DAYHHMM,      // Time Day HHMM  Mon 1234
 UNIT_DAY2HHMM,     // Time Day HHMM  1d 1234, 7d 2359
 UNIT_PERCENT,      // As percentage with % symbol
 UNIT_PERCENT2,     // As percentage with variable fractions
 UNIT_DATE3,        // calendar YYYYMMDD
 UNIT_SCIENTIFIC,   // scientific eg: 1.2345e-002
 UNIT_ABSTIMEC,     // C-style DAY MMM DD HH:MM:SS YYYY
 UNIT_PERCENT3,     // As percentage x.xxx%
 UNIT_PPM,          // Parts per million
 UNIT_PERCENT_NS,   // as percent without symbol
 UNIT_PPM_NS,       // as ppm without symbol
 UNIT_PERCENT6,     // As percentage x.xxxxxx%
 UNIT_VALUESPACED2, // value spaced without decimal
 UNIT_DATE4,        // Calendar dd MMM      (no year)
 UNIT_DATE5,        // Calendar dd MMM HHMM (no year)
 UNIT_HOURMIN,      // xxhr xxmin
 UNIT_OS_DATETIME,
 UNIT_OS_DATEONLY,
 UNIT_OS_TIME,
 UNIT_OS_CURRENCY,
 UNIT_OS_VALUE,
 UNIT_OS_PERCENT,
 UNIT_RGB,
 UNIT_VALUENODEC,
 UNIT_VALUE_F3,
 UNIT_VALUE_CF3,
 UNIT_PERCENT1,
 UNIT_DATE3TIME,     // Calendar YYYY-MM-DD HHMMSS
 UNIT_TIMEDHHMM,
 UNIT_TIMED0HMM,
 UNIT_DAY2HHMMCOLON,
 UNIT_WEEKDAYNOTIME, // week and day from 1
 UNIT_DAYFROM1,      // day from 1
 UNIT_DATE4TIME,     // calendar yyyymmddhhmmss

 // this counts unit modes
 UNIT_MODECOUNT,

 // special case - must be 255 and last
 UNIT_NULL       = 255
};

#endif

