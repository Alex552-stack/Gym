import React, { useEffect, useState } from 'react';
import { Container, Typography, Box } from '@mui/material';
import { Tier } from '../../../models/Tiers';
import TierEditor from '../../components/TierEditor';
import { useAppDispatch, useAppSelector } from '../../store/configureStore';
import { getTiers } from '../account/accountSlice';
import agent from '../../api/agent';
import QrGenerator from './QrGenerator';

const AdminPage: React.FC = () => {
  const tiersFromStore = useAppSelector(state => state.account.tiers);
  const dispatch = useAppDispatch();
  const [tiers, setTiers] = useState<Tier[]>([]);

  useEffect(() => {
    const fetchTiers = async () => {
      const resultAction = await dispatch(getTiers());
      if (getTiers.fulfilled.match(resultAction)) {
        const fetchedTiers = resultAction.payload as Tier[];
        setTiers(fetchedTiers);
      } else {
        console.error('Failed to fetch tiers:', resultAction.payload);
      }
    };

    fetchTiers();
  }, [dispatch]);

  useEffect(() => {
    if (tiersFromStore.length > 0) {
      setTiers(tiersFromStore);
    }
  }, [tiersFromStore]);

  const handleSave = (updatedTier: Tier) => {
    agent.Tiers.Edit(updatedTier);
    // Here you could also send the updated tier to your backend
    console.log('Updated Tiers:', updatedTier);
  };

  return (
    <Container>
      <QrGenerator/>
      <Typography variant="h4" gutterBottom>
        Manage Tiers
      </Typography>
      <Box display="grid" gridTemplateColumns="repeat(2, 1fr)" gap={4}>
        {tiers.map((tier) => (
          <TierEditor key={tier.Id} tier={tier} onSave={handleSave} />
        ))}
      </Box>
    </Container>
  );
};

export default AdminPage;
