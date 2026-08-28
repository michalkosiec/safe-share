import {useEffect, useRef, useState} from "react";
// @ts-expect-error no types for three
import * as THREE from 'three';
// @ts-expect-error no types for vanta
import FOG from 'vanta/src/vanta.fog';
import {Outlet} from "react-router-dom";

interface VantaEffect {
    destroy: () => void;
}

export default function LoginView() {
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

    return <div ref={vantaRef} className="min-h-screen flex justify-center items-center">
        <Outlet />
    </div>
}