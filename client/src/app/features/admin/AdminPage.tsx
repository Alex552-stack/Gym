import { useEffect, useState } from "react";
import agent from "../../api/agent";
import LoadinComponent from "../../layout/LoadingComponent";
import { Typography } from "@mui/material";
import QRCode from 'qrcode.react';

export default function AdminPage()
{
    const [qrData, setQrData] = useState(null);
    const [loading,setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        const fetchQrData = async () => {
            try {
                agent.Visists.GetQrCode().then((response) => {
                    console.log(response);
                    setQrData(response);
                    setLoading(false);
                });
            }
            catch(error : any)
            {
                setError(error.message);
                setLoading(false);
            }
        };

        fetchQrData();
    }, []);

    if(loading) return <LoadinComponent/>
    if(error) return <div>Error</div>

    const url = `${window.location.origin}/check-qr?code=${qrData}`;

    return (
        <>
            <Typography>
                Qr Code check
            </Typography>
            <div style={{'backgroundColor': 'white', 'textAlign': 'center'}}>
                <QRCode value={url} />
            </div>
        </>
    )
}