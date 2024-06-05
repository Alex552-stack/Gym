import { useEffect, useRef, useState } from "react";
import agent from "../../api/agent";
import LoadingComponent from "../../layout/LoadingComponent";
import dayjs, { Dayjs } from "dayjs";
import DateCalendarServerRequest from "../../components/DateCalendarWHighlight";
import { Box, Typography, LinearProgress, Grid } from "@mui/material";
import { User } from "../../../models/user";
import { useAppSelector } from "../../store/configureStore";
import {
  Chart as ChartJS,
  ChartConfiguration,
  ChartData,
} from "chart.js";
 import 'chart.js/auto';

const convertToDayjs = (dates: any) =>
  dates.map((date: { year: number; mounth: number; day: number }) =>
    dayjs(new Date(date.year, date.mounth - 1, date.day))
  );

  

export default function Account() {
  const [visits, setVisits] = useState<Dayjs[]>([]);
  const [lifetimeVisits, setLifetimeVisits] = useState(0);
  const [currentTierVisits, setCurrentTierVisits] = useState(0);
  const [nextTierVisits, setNextTierVisits] = useState(0);
  const [user, setUser] = useState<User | null>(null);
  const [loading, setLoading] = useState(true);
  const [chartData, setChartData] = useState<ChartData<'line'>>({
    labels: [],
    datasets: [],
  });
  const [shouldRenderChart, setShouldRenderChart] = useState(false);
  const chartRef = useRef<HTMLCanvasElement | null>(null);

  const userResponse = useAppSelector((state) => state.account);
  console.log(userResponse)

  const groupVisitsByMonth = (dates: { year: number; mounth: number }[]) => {
    const groupedData: { [key: string]: number } = {};
    dates.forEach((date) => {
      const monthKey = dayjs(new Date(date.year, date.mounth - 1)).format("MMMM");
      if (!groupedData[monthKey]) {
        groupedData[monthKey] = 0;
      }
      groupedData[monthKey]++;
    });
    // Array of all months
  const allMonths = Array.from({length: 12}, (_, i) => dayjs().month(i).format("MMMM"));

  // Map allMonths to groupedData
  const completeData = allMonths.map(month => groupedData[month] || 0);

  return completeData;
  };

  useEffect(() => {
    const fetchData = async () => {
      try {

        const visitsResponse = await agent.Visists.GetGymVisits();
        //console.log("test", userResponse.user?.unlockedTier.requiredCount);
        console.log(visitsResponse);

        const groupedVisits = groupVisitsByMonth(visitsResponse.dates);
        const labels = Object.keys(groupedVisits);
        const data = Object.values(groupedVisits);

        const chartData: ChartData<'line'> = {
          labels,
          datasets: [
            {
              label: "Visits",
              data,
              fill: false,
              backgroundColor: "rgb(75, 192, 192)",
              borderColor: "rgba(75, 192, 192, 0.2)",
            },
          ],
        };

        setChartData(chartData);
        //setShouldRenderLine(true);
        const currentDate = Date.now();
        const startT1 = new Date(currentDate);
        startT1.setDate(
          startT1.getDate() -
            (userResponse.user?.unlockedTier.requiredCount || 0)
        );
        const startT2 = new Date(currentDate);

        startT2.setDate(
          startT1.getDate() -
            (userResponse.user?.nextTier.requiredCount || 0)
        );
        await agent.Visists.GetInterval(startT1, new Date(currentDate)).then(
          (list) => {
            //console.log(list);
            setCurrentTierVisits(list.length);
            //console.log(currentTierVisits)
          }
        );
        //console.log(currentTierVisits)
        await agent.Visists.GetInterval(startT2, new Date(currentDate)).then(
          (list) => {
            console.log(list);
            setNextTierVisits(list.length);
            console.log(nextTierVisits)
          }
        );

        setLifetimeVisits(visitsResponse.count);
        console.log("Next:", nextTierVisits);
        console.log("current:", currentTierVisits);
        //console.log("Next:", nextTierVisits);
        const dates = convertToDayjs(visitsResponse.dates);
        setVisits(dates);

        setUser(userResponse.user);

        setLoading(false);
      } catch (error) {
        console.error("Error fetching data", error);
        setLoading(false);
      }
      setShouldRenderChart(true);
    };

    fetchData();
  }, []);

  useEffect(() => {
    if (shouldRenderChart) {
    if (chartRef.current) {
      const ctx = chartRef.current.getContext("2d");
      if (ctx) {
        const chartInstance = new ChartJS(ctx, {
          type: "line",
          data: chartData,
          options: {
            responsive: true,
            plugins: {
              legend: {
                position: "right",
              },
              title: {
                display: true,
                text: "Gym Visits",
              },
            },
            scales: {
              x: {
                type: 'category',
                labels: ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"],
              },
              y: {
                type: 'linear',
                min: 0,
                max: 32,
                ticks: {
                  stepSize: 1,
                },
              },
            },
          } as ChartConfiguration<'line'>['options'],
        });

        return () => {
          chartInstance.destroy();
        };
      }
    }
  }
  }, [shouldRenderChart, chartData]);


  if (loading) return <LoadingComponent />;

  const getProgress = () => {
    if (user && user.nextTier) {
      return (lifetimeVisits / user.nextTier.requiredCount) * 100;
    }
    return 0;
  };

  return (
    <Grid container spacing={2}>
      <Grid item xs={6}>
        <DateCalendarServerRequest highlightedDays={visits} />
      </Grid>
      <Grid item xs={6}>
        {user && (
          <Box mb={3}>
            <Typography variant="h6">Welcome, {user.email}</Typography>
            <Typography variant="body1" style={{ marginTop: "1rem" }}>
              Current Tier: {user.unlockedTier.name}
            </Typography>
            <Typography variant="body1" style={{ marginTop: "1rem" }}>
              {user.unlockedTier.description}
            </Typography>
            <Typography variant="body1" style={{ marginTop: "1rem" }}>
              {currentTierVisits} visits in{" "}
              {user.unlockedTier.timeToCompleteRequirement.split(".")[0]} days out of {user.unlockedTier.requiredCount} required days
            </Typography>
            <Typography variant="body1" style={{ marginTop: "1rem" }}>
              Next Tier: {user.nextTier.name}
            </Typography>
            <Typography variant="body1" style={{ marginTop: "1rem" }}>
              {user.nextTier.description}
            </Typography>
            <LinearProgress
              variant="determinate"
              value={getProgress()}
              style={{ height: 10, marginTop: "1rem" }}
            />
            <Typography variant="body2" style={{ marginTop: "1rem" }}>
              {nextTierVisits} / {user.nextTier.requiredCount} visits to reach
              the next tier
            </Typography>
            <Typography variant="body2">
              {user.nextTier.timeToCompleteRequirement.split(".")[0]} days to reach the milestone
            </Typography>
          </Box>
        )}
      </Grid>
      <div style={{ width: '50%', textAlign: 'center',marginLeft: '250px' }}>
      <canvas ref={chartRef} style={{ maxWidth: '70%', margin: '0 auto',width:'150%' }}></canvas>
    </div>

    </Grid>
  );
  
  
}
