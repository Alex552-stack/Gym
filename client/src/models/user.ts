import { Tier } from "./Tiers";

export interface User{
    email:string;
    token: string;
    roles?: string[];
    unlockedTier: Tier;
    nextTier: Tier;

}