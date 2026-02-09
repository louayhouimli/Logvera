"use client";

import Image from "next/image";
import Link from "next/link";

import { FcGoogle } from "react-icons/fc";

import { Background } from "@/components/background";
import { Button } from "@/components/ui/button";
import { Card, CardContent, CardHeader } from "@/components/ui/card";
import { Input } from "@/components/ui/input";
import { useRouter } from "next/navigation";
import { useRegisterMutation } from "@/mutations/auth.mutations";
import {
  RegisterFormValues,
  registerSchema,
} from "@/lib/schemas/register.schema";
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { error } from "console";

const Signup = () => {
  const router = useRouter();
  const registerMutation = useRegisterMutation();
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<RegisterFormValues>({
    resolver: zodResolver(registerSchema),
  });

  const onSubmit = (data: RegisterFormValues) => {
    registerMutation.mutate(data);
  };
  return (
    <Background>
      <section className="py-28 lg:pt-44 lg:pb-32">
        <div className="container">
          <div className="flex flex-col gap-4">
            <Card className="mx-auto w-full max-w-sm">
              <CardHeader className="flex flex-col items-center space-y-0">
                <Link href="/" className="mb-7">
                  <Image
                    src="/logo.svg"
                    alt="logo"
                    width={38}
                    height={38}
                    className="mb-7 dark:invert"
                  />
                </Link>
                <p className="mb-2 text-2xl font-bold">Create your account</p>
                <p className="text-muted-foreground">
                  Sign up in less than 2 minutes.
                </p>
              </CardHeader>
              <CardContent>
                <form onSubmit={handleSubmit(onSubmit)}>
                  <div className="grid gap-4">
                    {/* <Input type="text" placeholder="Enter your name" /> */}
                    <Input
                      type="email"
                      placeholder="Enter your email"
                      {...register("email")}
                    />
                    {errors.email && (
                      <p className="text-sm text-red-500">
                        {errors.email.message}
                      </p>
                    )}
                    <div>
                      <Input
                        type="password"
                        placeholder="Enter your password"
                        {...register("password")}
                      />
                      {errors.password && (
                        <p className="text-sm text-red-500">
                          {errors.password.message}
                        </p>
                      )}
                    </div>
                    <div>
                      <Input
                        type="password"
                        placeholder="Confirm your password"
                        {...register("confirmPassword")}
                      />
                      {errors.confirmPassword && (
                        <p className="text-sm text-red-500">
                          {errors.confirmPassword.message}
                        </p>
                      )}
                    </div>
                    <Button type="submit" className="mt-2 w-full">
                      Create an account
                    </Button>
                    <Button variant="outline" className="w-full">
                      <FcGoogle className="mr-2 size-5" />
                      Sign up with Google
                    </Button>
                  </div>
                  <div className="text-muted-foreground mx-auto mt-8 flex justify-center gap-1 text-sm">
                    <p>Already have an account?</p>
                    <Link href="/login" className="text-primary font-medium">
                      Log in
                    </Link>
                  </div>
                </form>
              </CardContent>
            </Card>
          </div>
        </div>
      </section>
    </Background>
  );
};

export default Signup;
