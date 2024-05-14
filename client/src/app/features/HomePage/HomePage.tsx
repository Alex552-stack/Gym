import { Card, CardContent, CardMedia, Grid, Typography } from "@mui/material"

export default function HomePage()
{
    return (
        <Grid container spacing={3} sx={{ padding: 3 }}>
        <Grid item xs={12}>
          <Card sx={{ maxWidth: 345, bgcolor: 'background.paper' }}>
            <CardMedia
              component="img"
              height="140"
              image="https://source.unsplash.com/random/345x140?gym"
              alt="Gym"
            />
            <CardContent>
              <Typography gutterBottom variant="h5" component="div">
                Welcome to Our Gym
              </Typography>
              <Typography variant="body2" color="text.secondary">
                Get fit and healthy with us. Join our community and start your journey today!
              </Typography>
            </CardContent>
          </Card>
        </Grid>

        {/* Example Feature Cards */}
        {['Workout Programs', 'Personal Training', 'Nutrition Plans'].map((feature, index) => (
          <Grid item xs={12} sm={6} md={4} key={index}>
            <Card sx={{ bgcolor: 'background.paper' }}>
              <CardContent>
                <Typography variant="h5" component="div">
                  {feature}
                </Typography>
                <Typography variant="body2" color="text.secondary">
                  Explore our {feature.toLowerCase()} designed to help you achieve your fitness goals.
                </Typography>
              </CardContent>
            </Card>
          </Grid>
        ))}
      </Grid>
    )
}