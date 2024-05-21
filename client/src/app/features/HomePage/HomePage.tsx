import { Box, Grid, Typography } from '@mui/material';
import gif from '../../../Images/giphy.gif'; 
 // Adjust the path if necessary
import { useState, useEffect } from 'react';

export default function HomePage() {
  const [isSmallScreen, setIsSmallScreen] = useState(false);

  useEffect(() => {
    const handleResize = () => {
      setIsSmallScreen(window.innerWidth <= 768); // Adjust the breakpoint as needed
    };

    // Call handleResize initially
    handleResize();

    window.addEventListener('resize', handleResize);

    // Cleanup the event listener on unmount
    return () => window.removeEventListener('resize', handleResize);
  }, []);

  return (
    <div>
      <Grid
        container
        justifyContent="center"
        alignItems="center"
        sx={{
          height: '90vh', // Adjust for the height of the navbar
          width: '100%',
          margin: 0,
          padding: 0,
          backgroundImage: `url(${gif})`,
          backgroundSize: 'cover',
          backgroundRepeat: 'no-repeat'
        }}
      >
        <Grid item xs={12} sx={{ height: '100%', position: 'relative' }}>
          <Box style={{color:'black', opacity: '80%'}}>
            <Typography
              variant="h3"
              component="div"
              sx={{
                position: 'absolute',
                top: '40%',
                left: '21%',
                transform: 'translate(-50%, -50%)',
                fontSize: isSmallScreen ? '60px' : '100px', 
                color: 'transparent',
                backgroundImage: 'linear-gradient(#fff, #fff)',
                backgroundRepeat: 'no-repeat',
                WebkitBackgroundClip: 'text',
                backgroundPosition: '-800px 0',
                animation: 'backcolor 4s linear infinite alternate',
                '@keyframes backcolor': {
                  '100%': {
                    backgroundPosition: '0 0',
                  },
                },
              }}
            >
               WELCOME!
            </Typography>
          </Box>
        </Grid>
      </Grid>
    </div>
  );
}
