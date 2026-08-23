import {type SubmitEvent, useEffect, useRef, useState} from "react";
// @ts-expect-error no types for three
import * as THREE from 'three';
// @ts-expect-error no types for vanta
import FOG from 'vanta/src/vanta.fog';
import { User, Lock } from "lucide-react";
import {Link} from "react-router-dom";

interface VantaEffect {
    destroy: () => void;
}

export default function Login() {
    const vantaRef = useRef<HTMLDivElement>(null);
    const [vantaEffect, setVantaEffect] = useState<VantaEffect | null>(null);

    useEffect(() => {
        if (!vantaEffect && vantaRef) {
            setVantaEffect(FOG({
                el: vantaRef.current,
                THREE: THREE,
                mouseControls: true,
                touchControls: true,
                gyroControls: false,
                minHeight: 200.00,
                minWidth: 200.00,
                highlightColor: 0xd9c3d5,
                midtoneColor: 0x6179cc,
                lowlightColor: 0x437f9d,
                baseColor: 0xffd8d8
            }));
        }
        return () => {
            if (vantaEffect)
                vantaEffect.destroy();
        }
    }, [vantaEffect]);
    const handleSubmit = (e: SubmitEvent<HTMLFormElement>) => {
        e.preventDefault();
        console.log("Login clicked!")
    }
    return <div ref={vantaRef} className="min-h-screen flex justify-center items-center">
        <div className="flex flex-col w-160 p-6 bg-[rgba(255,255,255,0.4)] rounded-xl backdrop-blur-md border border-white/10 shadow-2xl">
            <div>
                <h1 className="text-3xl font-bold tracking-wider">Log in</h1>
            </div>
            <form onSubmit={handleSubmit} className="flex flex-col h-full space-y-6 mt-5">
                <div>
                    <label>User name</label>
                    <div className="flex justify-between mt-2 bg-[rgba(255,255,255,0.6)] h-10 rounded-lg items-center pl-2 gap-4">
                        <User />
                        <input type="text" placeholder="Your login" className="w-full"/>
                    </div>
                </div>
                <div>
                    <label>Password</label>
                    <div className="flex justify-between mt-2 bg-[rgba(255,255,255,0.6)] h-10 rounded-lg items-center pl-2 gap-4">
                        <Lock />
                        <input type="password" placeholder="Your password" className="w-full"/>
                    </div>
                </div>
                <div>
                    <button type="submit" className="w-full rounded-lg text-white bg-black/40 cursor-pointer h-10 mt-5 font-bold hover:bg-black/50 hover:text-white/90">Enter</button>
                </div>
                <div className="flex justify-center">
                    <p>Don't have an account? <Link to="/register" className="font-bold hover:underline">Register</Link></p>
                </div>
            </form>
        </div>
    </div>
}