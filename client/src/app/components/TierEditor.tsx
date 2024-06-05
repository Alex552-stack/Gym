import React, { useState } from 'react';
import { Box, TextField, Button, Paper, Typography } from '@mui/material';
import { Tier } from '../../models/Tiers';

interface TierEditorProps {
  tier: Tier;
  onSave: (updatedTier: Tier) => void;
}

const TierEditor: React.FC<TierEditorProps> = ({ tier, onSave }) => {
  const [editedTier, setEditedTier] = useState<Tier>({ ...tier });

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setEditedTier({
      ...editedTier,
      [name]: name === 'RequiredCount' || name === 'Id' ? parseInt(value, 10) : value,
    });
  };

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    onSave(editedTier);
  };

  return (
    <Paper elevation={3} style={{ padding: '16px', marginTop: '16px' }}>
      <Typography variant="h6" gutterBottom>
        Edit Tier
      </Typography>
      <form onSubmit={handleSubmit}>
        <Box display="flex" flexDirection="column" gap={2}>
          <TextField
            label="Id"
            name="Id"
            type="number"
            value={editedTier.Id}
            onChange={handleChange}
            disabled
          />
          <TextField
            label="Name"
            name="Name"
            value={editedTier.name}
            onChange={handleChange}
          />
          <TextField
            label="DateTime"
            name="DateTime"
            value={editedTier.timeToCompleteRequirement}
            onChange={handleChange}
          />
          <TextField
            label="Required Count"
            name="RequiredCount"
            type="number"
            value={editedTier.requiredCount}
            onChange={handleChange}
          />
          <TextField
            label="Description"
            name="Description"
            value={editedTier.description}
            onChange={handleChange}
            multiline
            rows={4}
          />
          <Button type="submit" variant="contained" color="primary">
            Save
          </Button>
        </Box>
      </form>
    </Paper>
  );
};

export default TierEditor;
