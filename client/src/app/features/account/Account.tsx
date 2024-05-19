import { DatePicker } from '@mui/x-date-pickers/DatePicker';
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';
import { LocalizationProvider } from '@mui/x-date-pickers/LocalizationProvider/LocalizationProvider';
import { DateCalendar } from '@mui/x-date-pickers/DateCalendar';

export default function Account() {
  return (
    <LocalizationProvider dateAdapter={AdapterDayjs} adapterLocale='ro'>
            <DateCalendar/>
    </LocalizationProvider>
  );
}