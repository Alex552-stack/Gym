import * as React from 'react';
import dayjs, { Dayjs } from 'dayjs';
import Badge from '@mui/material/Badge';
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';
import { LocalizationProvider } from '@mui/x-date-pickers/LocalizationProvider';
import { PickersDay, PickersDayProps } from '@mui/x-date-pickers/PickersDay';
import { DateCalendar } from '@mui/x-date-pickers/DateCalendar';
import { DayCalendarSkeleton } from '@mui/x-date-pickers/DayCalendarSkeleton';

function ServerDay(props: PickersDayProps<Dayjs> & { highlightedDates?: Dayjs[] }) {
  const { highlightedDates = [], day, outsideCurrentMonth, ...other } = props;

  const isSelected =
    !props.outsideCurrentMonth && highlightedDates.some(highlightedDay => highlightedDay.isSame(day, 'day'));

  return (
    <Badge
      key={props.day.toString()}
      overlap="circular"
      badgeContent={isSelected ? '🌚' : undefined}
    >
      <PickersDay {...other} outsideCurrentMonth={outsideCurrentMonth} day={day} />
    </Badge>
  );
}

interface DateCalendarWHighlight {
  highlightedDates: Dayjs[];
}

export default function DateCalendarWHighlight({ highlightedDates }: DateCalendarWHighlight) {
  const [currentHighlightedDates, setCurrentHighlightedDates] = React.useState(highlightedDates);

  React.useEffect(() => {
    setCurrentHighlightedDates(highlightedDates);
  }, [highlightedDates]);

  const handleMonthChange = (date: Dayjs) => {
    // Optionally handle month change if needed
  };

  return (
    <LocalizationProvider dateAdapter={AdapterDayjs}>
      <DateCalendar
        defaultValue={dayjs('2022-04-17')}
        onMonthChange={handleMonthChange}
        slots={{
          day: ServerDay,
        }}
        slotProps={{
          day: {
            highlightedDates: currentHighlightedDates,
          } as any,
        }}
      />
    </LocalizationProvider>
  );
}
