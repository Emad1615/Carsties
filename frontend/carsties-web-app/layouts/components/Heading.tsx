"use client";
import { motion } from "motion/react";

type Props = {
  title: string;
  subtitle?: string;
  textColor?: string;
};
export default function Heading({ title, subtitle, textColor }: Props) {
  return (
    <div
      className={`flex justify-center items-center flex-col gap-2 border-b border-gray-200 pb-4 mb-8 `}
    >
      <span
        className={`text-2xl font-bold ${textColor ? `text-${textColor}-500` : ""}`}
      >
        {title}
      </span>
      {subtitle && (
        <div>
          <motion.div
            animate={{ x: ["0px", "15px", "-15px", "15px", "-15px", "0px"] }}
            transition={{
              duration: 5,
              repeat: Infinity,
              repeatType: "loop",
              ease: "linear",
            }}
            className="text-sm  text-gray-500 font-semibold"
          >
            {subtitle}
          </motion.div>
        </div>
      )}
    </div>
  );
}
