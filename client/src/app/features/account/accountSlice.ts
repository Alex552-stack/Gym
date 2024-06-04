import { createAsyncThunk, createSlice, isAnyOf } from "@reduxjs/toolkit"; // External library
import { FieldValues } from "react-hook-form"; // External library
import { toast } from "react-toastify"; // External library
import { User } from "../../../models/user"; // Your own module
import agent from "../../api/agent"; // Your own module
import { router } from "../../Router/Routes"; // Your own module
import { Tier } from "../../../models/Tiers";


interface AccountState {
    user: User | null;
    qrCode: string | null;
    pendingQrCode: string | null;
    tiers: Tier[]; // Store tiers as an array
    roles?: string[] | null;
}

interface ScanQrCodePayload {
    qrCode: string ;
}


const initialState: AccountState = {
    user: null,
    qrCode: null,
    pendingQrCode: null,
    roles: null,
    tiers: [], // Initialize as an empty array
};

export const getTiers = createAsyncThunk<Tier[]>(
    'tiers/get',
    async (_, thunkAPI) => {
        const tiers = await agent.Tiers.GetAll();
        return tiers.map((tier: any) => ({
            Id: tier.id,
            Name: tier.name,
            RequiredCount: tier.requiredCount,
            Description: tier.description,
            TimeToCompleteRequirement: tier.TimeToCompleteRequirement
        }));
    }
);


export const scanQrCode = createAsyncThunk<void, ScanQrCodePayload>(
    'account/scanQrCode',
    async (data, thunkAPI) => {
        const state = thunkAPI.getState() as { account: AccountState };
        const user = state.account.user;
        console.log(user);
        if (!user) {
            thunkAPI.dispatch(setPendingQrCode(data.qrCode)); // Save the QR code
            toast.info("You need to log in to scan the QR code");
            router.navigate('/login'); // Redirect to login
            return;
        }

        try {
            const response = await agent.Visists.ScanQrCode(data.qrCode);
            thunkAPI.dispatch(setQrCode(response.qrCode));
            toast.success('QR code scanned successfully');
        } catch (error: any) {
            return thunkAPI.rejectWithValue({ error: error.data });
        }
    }
);

export const signInUser = createAsyncThunk<User, FieldValues>(
    'account/signInUser',
    async (data, thunkAPI) => {
        try{
            const userDto = await agent.Account.login(data);
            const {...user} = userDto;
            localStorage.setItem('user', JSON.stringify(user));
             await thunkAPI.dispatch(fetchCurrentUser()); // Wait for fetchCurrentUser to ensure user state is updated
            const state = thunkAPI.getState() as { account: AccountState };
            console.log(state.account);
            if (state.account.pendingQrCode) {
                thunkAPI.dispatch(scanQrCode({ qrCode: state.account.pendingQrCode }));
                thunkAPI.dispatch(setPendingQrCode(null));
            }

            await thunkAPI.dispatch(getTiers());

            return user;
        } catch (error : any) {
            return thunkAPI.rejectWithValue({error: error.data})
        }
    }
)


export const fetchCurrentUser = createAsyncThunk<User>(
    'account/fetchCurrentUser',
    async (_, thunkAPI) => {
        thunkAPI.dispatch(setUser(JSON.parse(localStorage.getItem('user')!)))
        thunkAPI.dispatch(getTiers());
        try {
            const userDto = await agent.Account.currentUser();
            const {...user} = userDto;
            localStorage.setItem('user', JSON.stringify(user));
            if(!user.emailConfirmed) toast.warning("Your email is not confirmed");
            return user;
        } catch (error: any) {
            return thunkAPI.rejectWithValue({ error: error.data })
        }
    },
    {
        condition: () => {
            if(!localStorage.getItem('user')) return false;
        }
    }
)

export const accountSlice = createSlice({
    name: 'account',
    initialState,
    reducers: {
        signOut: (state) => {
            state.user = null;
            localStorage.removeItem('user');
            router.navigate('/');
        },
        setUser: (state,action) => {
            let claims = JSON.parse(atob(action.payload.token.split('.')[1]));
            let roles = claims['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
            toast.success("Te-ai conectat cu succes");
            state.user = {...action.payload, roles: typeof(roles) === 'string' ? [roles] : roles};
        },
        setQrCode: (state, action) => {
            state.qrCode = action.payload;
        },
        setPendingQrCode: (state, action) => {
            state.pendingQrCode = action.payload;
        }
    },
    extraReducers: (builder => {
        builder.addCase(fetchCurrentUser.rejected, (state) => {
            state.user = null;
            state.qrCode = null;
            state.pendingQrCode = null; // Reset pending QR code on fetch current user rejection
            localStorage.removeItem('user');
            toast.error('Sesiune expirata - te rog reconecteaza-te');
            router.navigate('/');
        });
        builder.addCase(getTiers.fulfilled, (state, action) => {
            state.tiers = action.payload;
        });
        builder.addMatcher(isAnyOf(signInUser.fulfilled, fetchCurrentUser.fulfilled), (state, action) => {
            let claims = JSON.parse(atob(action.payload.token.split(".")[1]));
            let roles = claims['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
            state.user = {...action.payload, roles : typeof(roles) === "string" ? [roles] : roles};
        });
        builder.addMatcher(isAnyOf(signInUser.rejected,), (_state, action) => {
            console.log(action.payload);
        });
    })
})

export const {signOut, setUser, setQrCode, setPendingQrCode} = accountSlice.actions;
export default accountSlice.reducer;
