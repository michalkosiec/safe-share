import {type SubmitEvent, useEffect, useRef, useState} from "react";
import {Link} from "react-router-dom";
// @ts-expect-error no types for three
import * as THREE from 'three';
// @ts-expect-error no types for vanta
import FOG from 'vanta/src/vanta.fog';
import {CheckCircle, Lock, User} from "lucide-react";

interface VantaEffect {
    destroy: () => void;
}

export default function Register() {
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
    const handleSubmit = (e: SubmitEvent) => {
        e.preventDefault();
        console.log("Register clicked!")
    }
    return <div ref={vantaRef} className="min-h-screen flex justify-center items-center">
        <div className="flex flex-col w-160 p-6 bg-[rgba(0,0,0,0.6)] rounded-xl backdrop-blur-xl border border-black/10 shadow-2xl text-white">
            <div>
                <h1 className="text-3xl font-bold tracking-wider">Register</h1>
            </div>
            <form onSubmit={handleSubmit} className="flex flex-col h-full space-y-6 mt-5">
                <div>
                    <label>User name</label>
                    <div className="flex justify-between mt-2 bg-[rgba(255,255,255,0.1)] h-10 rounded-lg items-center pl-2 gap-4">
                        <User />
                        <input
                            type="text"
                            placeholder="Your login"
                            className="w-full bg-transparent outline-none"
                            required
                        />
                    </div>
                </div>
                <div>
                    <label>Password</label>
                    <div className="flex justify-between mt-2 bg-[rgba(255,255,255,0.1)] h-10 rounded-lg items-center pl-2 gap-4">
                        <Lock />
                        <input
                            type="password"
                            placeholder="Your password"
                            className="w-full bg-transparent outline-none"
                            required
                        />
                    </div>
                </div>
                <div>
                    <label>Confirm Password</label>
                    <div className="flex justify-between mt-2 bg-[rgba(255,255,255,0.1)] h-10 rounded-lg items-center pl-2 gap-4">
                        <CheckCircle />
                        <input
                            type="password"
                            placeholder="Repeat your password"
                            className="w-full bg-transparent outline-none"
                            required
                        />
                    </div>
                </div>
                <div>
                    <button
                        type="submit"
                        className="w-full rounded-lg bg-[rgba(227,197,215)] text-black cursor-pointer h-10 mt-5 font-bold hover:bg-[rgba(227,197,215,0.8)] hover:text-black/60 disabled:opacity-50 disabled:cursor-not-allowed"
                    >
                        Enter
                    </button>
                </div>
                <div className="flex justify-center text-sm gap-2">
                    <p>Already have an account?</p><Link to="/login" className="font-bold hover:underline">Log in</Link>
                </div>
            </form>
        </div>
    </div>
}