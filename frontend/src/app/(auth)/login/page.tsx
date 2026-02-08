"use client";
import Image from "next/image";
import Link from "next/link";

import { FcGoogle } from "react-icons/fc";

import { Background } from "@/components/background";
import { Button } from "@/components/ui/button";
import { Card, CardContent, CardHeader } from "@/components/ui/card";
import { Checkbox } from "@/components/ui/checkbox";
import { Input } from "@/components/ui/input";
import { useRouter } from "next/navigation";
import { useLoginMutation } from "@/mutations/auth.mutations";
import { useForm } from "react-hook-form";
import { LoginFormValues, loginSchema } from "@/lib/schemas/login.schema";
import { zodResolver } from "@hookform/resolvers/zod";

const Login = () => {
  const router = useRouter();
  const loginMutation = useLoginMutation();
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<LoginFormValues>({
    resolver: zodResolver(loginSchema),
  });

  const onSubmit = (data: LoginFormValues) => {
    loginMutation.mutate(data, {
      onSuccess: () => {
        router.push("/");
      },
    });
  };
  return (
    <Background>
      <section className="py-28 lg:pt-44 lg:pb-32">
        <div className="container">
          <div className="flex flex-col gap-4">
            <Card className="mx-auto w-full max-w-sm">
              <CardHeader className="flex flex-col items-center space-y-0">
                <Image
                  src="/logo.svg"
                  alt="logo"
                  width={38}
                  height={38}
                  className="mb-7 dark:invert"
                />
                <p className="mb-2 text-2xl font-bold">Welcome back!</p>
                <p className="text-muted-foreground">
                  Please enter your details.
                </p>
              </CardHeader>
              <form onSubmit={handleSubmit(onSubmit)}>
                <CardContent>
                  <div className="grid gap-4">
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
                    <div className="flex justify-between">
                      <div className="flex items-center space-x-2">
                        <Checkbox
                          id="remember"
                          className="border-muted-foreground"
                        />
                        <label
                          htmlFor="remember"
                          className="text-sm leading-none font-medium peer-disabled:cursor-not-allowed peer-disabled:opacity-70"
                        >
                          Remember me
                        </label>
                      </div>
                      <a href="#" className="text-primary text-sm font-medium">
                        Forgot password
                      </a>
                    </div>
                    <Button
                      disabled={loginMutation.isPending}
                      type="submit"
                      className="mt-2 w-full"
                    >
                      {loginMutation.isPending ? "Signing in..." : "Sign in"}
                    </Button>
                    <Button variant="outline" className="w-full">
                      <FcGoogle className="mr-2 size-5" />
                      Sign in with Google
                    </Button>
                  </div>
                  <div className="text-muted-foreground mx-auto mt-8 flex justify-center gap-1 text-sm">
                    <p>Don&apos;t have an account?</p>
                    <Link href="/signup" className="text-primary font-medium">
                      Sign up
                    </Link>
                  </div>
                </CardContent>
              </form>
            </Card>
          </div>
        </div>
      </section>
    </Background>
  );
};

export default Login;
