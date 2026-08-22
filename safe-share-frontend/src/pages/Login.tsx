import {useEffect, useRef, useState} from "react";
// @ts-ignore
import * as THREE from 'three';
// @ts-ignore
import FOG from 'vanta/src/vanta.fog';
import { User, Lock } from "lucide-react";

export default function Login() {
    const vantaRef = useRef<HTMLDivElement>(null);
    const [vantaEffect, setVantaEffect] = useState<any>(null);

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

            return () => {
                if (vantaEffect)
                    vantaEffect.destroy();
            }
        }
    }, [vantaEffect]);
    const handleSubmit = (e: React.FormEvent) => {
        e.preventDefault();
        console.log("Login clicked!")
    }
    return <div ref={vantaRef} className="min-h-screen flex justify-center items-center">
        <div className="flex flex-col w-160 h-120 p-6 bg-[rgba(1,1,1,0.1)] rounded-xl backdrop-blur-md border border-white/10 text-white shadow-2xl">
            <div>
                <h1 className="text-3xl font-bold tracking-wider">Log in</h1>
            </div>
            <form onSubmit={handleSubmit} className="flex flex-col h-full space-y-6">
                <div>
                    <label>User name</label>
                    <div className="flex justify-between mt-2 bg-[rgba(1,1,1,0.15)] h-10 rounded-lg">
                        <User />
                        <input type="text" placeholder="Your login"/>
                    </div>
                </div>
                <div>
                    <label>Password</label>
                    <div className="flex justify-between mt-2 bg-[rgba(1,1,1,0.15)] h-10 rounded-lg">
                        <Lock />
                        <input type="password" placeholder="Your password"/>
                    </div>
                </div>
                <div>
                <button type="submit">Enter</button>
                </div>
            </form>
        </div>
    </div>
}